Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
Imports System.Xml.Xsl
Imports System.Xml.Schema
Imports System.Xml.XPath.XPathItem
Imports System.Xml.XPath.XPathNavigator
Imports System.Xml.Serialization
Imports System.Collections
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports FirmaSAT.Sat
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Util
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Security
Imports System.Runtime.InteropServices
Imports System.Web.Services.Protocols
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Net.Security
Imports Ionic.Zip

Partial Public Class ComplementoDePagos
    Inherits System.Web.UI.Page

    Private iva As Decimal = 0
    Private total As Decimal = 0
    Private importesindescuento As Decimal = 0
    Private importetasacero As Decimal = 0
    Private totaldescuento As Decimal = 0
    Private tieneIvaTasaCero As Boolean = False
    Private tieneIva16 As Boolean = False
    Private archivoLlavePrivada As String = ""
    Private contrasenaLlavePrivada As String = ""
    Private archivoCertificado As String = ""
    Private _selloCFD As String = ""
    Private _cadenaOriginal As String = ""

    Private tipocontribuyenteid As Integer = 0
    Private tipoprecioid As Integer
    Private cadOrigComp As String
    '**************************
    Private m_xmlDOM As New XmlDocument
    Const URI_SAT = "http://www.sat.gob.mx/cfd/4"
    Private listErrores As New List(Of String)
    Private Comprobante As XmlNode
    Public Const NOMBRE_XSLT = "cadenaoriginal_3_3.xslt"
    Public Const DIR_SAT = "\SAT\"
    Const xsi = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd"
    Private Url As Integer = 0
    Dim UUID As String = ""
    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb
    Private data As Byte()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle
        '
        chkAll.Attributes.Add("onclick", "checkedAll(" & Me.Form.ClientID.ToString & ");")


        '
        If Not IsPostBack Then
            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsSelectionLegend.Text = Resources.Resource.lblClientsSelectionLegend
            lblClientItems.Text = "Facturas Pendientes de Pago"
            lblResume.Text = Resources.Resource.lblResume

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            Dim ObjCat As New DataControl
            ObjCat.Catalogo(cmbCliente, "EXEC pMisClientes @cmd=1", 0)
            ObjCat.Catalogo(cmbTipoPago, "select top 1 id, nombre from tblTipoPagos order by nombre", 0)
            ObjCat.Catalogo(cmbFormaPago, "select id, id + ' - ' + nombre as nombre from tblFormaPago where id not in (99) order by nombre", 0)
            ObjCat.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by nombre", 1)
            ObjCat = Nothing

            If cmbMoneda.SelectedValue <> 1 Then
                txtTipoCambio.Enabled = True
                valTipoCambio.Enabled = True
            Else
                txtTipoCambio.Text = 0
                txtTipoCambio.Enabled = False
                valTipoCambio.Enabled = False
            End If

            cmbCliente.Text = Resources.Resource.cmbEmptyMessage
            lblTotal.Text = Resources.Resource.lblTotal
            btnCreateInvoice.Text = Resources.Resource.btnCreateInvoice
            btnCancelInvoice.Text = Resources.Resource.btnCancelInvoice
            '
            '   Protege contra doble clic la creación de la factura
            '
            btnCreateInvoice.Attributes.Add("onclick", "javascript:" + btnCreateInvoice.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnCreateInvoice, ""))
            '
            panelItemsRegistration.Visible = False
            itemsList.Visible = False
            panelResume.Visible = False

            CrearTablaTemp()

            If Not String.IsNullOrEmpty(Request("id")) Then
                Session("CFD") = Request("id")
                Session("PAGOCFD") = 0
                Call CargaCFD()
                Call CargarSaldoPendiente()
            Else
                Session("CFD") = 0
                Session("PAGOCFD") = 0
            End If

        End If
    End Sub

    Private Sub CrearTablaTemp()
        Dim dt As New DataTable()
        dt.Columns.Add("id")
        dt.Columns.Add("fecha")
        dt.Columns.Add("serie")
        dt.Columns.Add("folio")
        dt.Columns.Add("uuid")
        dt.Columns.Add("monedaFactura")
        dt.Columns.Add("total")
        dt.Columns.Add("saldo")
        dt.Columns.Add("saldoanterior")
        dt.Columns.Add("monto")
        dt.Columns.Add("montomxn")
        dt.Columns.Add("chkcfdid")
        Session("TmpDetalleComplemento") = dt
    End Sub

    Private Sub CargaCFD()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=10, @cfdid='" & Session("CFD").ToString & "'", conn)
        Dim clienteid As Long = 0
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()


            panelItemsRegistration.Visible = True
            panelResume.Visible = True

            If rs.Read() Then

                cmbCliente.SelectedValue = rs("clienteid")
                clienteid = rs("clienteid")

                If cmbCliente.SelectedValue > 0 Then
                    Call DisplayItems()
                    panelItemsRegistration.Visible = True
                    itemsList.Visible = True
                End If

            End If

            rs.Close()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try
        '
        Call CargaCliente(clienteid)
        '
    End Sub

    Private Sub DisplayItems()
        itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
        Session("TmpDetalleComplemento") = ObtenerItems().Tables(0)
        itemsList.DataSource = Session("TmpDetalleComplemento")
        itemsList.DataBind()
    End Sub

    Private Sub CargaCliente(ByVal ClienteId As Long)
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=2, @clienteid='" & ClienteId.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            tipoprecioid = 0
            panelItemsRegistration.Visible = True

            If rs.Read() Then
                Dim ObjData As New DataControl
                ObjData.Catalogo(cmbFormaPago, "select id, id + ' - ' + nombre as nombre from tblFormaPago where id not in (99) order by nombre", rs("formapagoid"))
                ObjData.Catalogo(cmbCtaOrdenante, "select id, isnull(numctapago,'') as numctapago FROM tblCuentasBancarias where clienteid='" & cmbCliente.SelectedValue & "' order by numctapago", rs("idctaOrdenante"))
                ObjData.Catalogo(cmbCtaBeneficiario, "select id, isnull(numctapago,'') as numctapago FROM tblCuentasBeneficiario order by banco", rs("idctaBeneficiario"))
                ObjData = Nothing

                txtRfcBeneficiario.Text = rs("rfcbeneficiario")
                txtRFCCtaOrdenante.Text = rs("rfcctaordenante")

                If cmbFormaPago.SelectedValue.Length > 0 Then
                    If cmbFormaPago.SelectedValue = "02" Or cmbFormaPago.SelectedValue = "03" Or cmbFormaPago.SelectedValue = "04" Or cmbFormaPago.SelectedValue = "05" Or cmbFormaPago.SelectedValue = "06" Or cmbFormaPago.SelectedValue = "08" Or cmbFormaPago.SelectedValue = "17" Or cmbFormaPago.SelectedValue = "28" Or cmbFormaPago.SelectedValue = "29" Then
                        panelRecepcionPago.Visible = True
                    Else
                        panelRecepcionPago.Visible = False
                    End If
                    Validadores()
                End If
            End If

            rs.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub Validadores()
        Select Case cmbFormaPago.SelectedValue
            Case "02" ' Transferencia o Dinero electrónico
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "03" ' Transferencia o Dinero electrónico
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "04" ' Transferencia o Dinero electrónico
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "05" ' Transferencia o Dinero electrónico
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "06"
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "08"
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "17"
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "28"
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
            Case "29"
                RequiredFieldValidator1.Enabled = True
                RequiredFieldValidator2.Enabled = True
                'RequiredFieldValidator3.Enabled = True
                RequiredFieldValidator4.Enabled = True
                RequiredFieldValidator5.Enabled = True
                RequiredFieldValidator6.Enabled = True
        End Select
    End Sub

    Function ObtenerItems() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim cmd As New SqlDataAdapter("EXEC pComplementoDePago @cmd=1, @clienteid='" & cmbCliente.SelectedValue & "', @tipoid='1'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        CargarSaldoPendiente()
        RadWindow1.VisibleOnPageLoad = False
    End Sub

    Public Sub CargarSaldoPendiente()
        Dim i As Integer = 0

        Call ValidarExistComplemento()

        Session("TmpDetalleComplemento").Rows.Clear()

        For Each dataItem As Telerik.Web.UI.GridDataItem In itemsList.MasterTableView.Items
            Dim detalleid As String = dataItem.GetDataKeyValue("id").ToString
            Dim lblfechaCFDI As Label = DirectCast(dataItem.FindControl("lblfechaCFDI"), Label)
            Dim lbltotalCFDI As Label = DirectCast(dataItem.FindControl("lbltotalCFDI"), Label)
            Dim lblSerie As Label = DirectCast(dataItem.FindControl("lblSerie"), Label)
            Dim lblFolio As Label = DirectCast(dataItem.FindControl("lblFolio"), Label)
            Dim lblUUID As Label = DirectCast(dataItem.FindControl("lblUUID"), Label)
            Dim lblMonedaFactura As Label = DirectCast(dataItem.FindControl("lblMonedaFactura"), Label)
            Dim lblSaldo As Label = DirectCast(dataItem.FindControl("lblSaldo"), Label)
            Dim lblSaldoAnterior As Label = DirectCast(dataItem.FindControl("lblSaldoAnterior"), Label)
            Dim txtMonto As RadNumericTextBox = DirectCast(dataItem.FindControl("txtMonto"), RadNumericTextBox)
            Dim txtImportePesos As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImportePesos"), RadNumericTextBox)
            Dim chkcfdid As System.Web.UI.WebControls.CheckBox = DirectCast(dataItem.FindControl("chkcfdid"), System.Web.UI.WebControls.CheckBox)

            Dim monto As Decimal = txtMonto.Text
            Dim saldo As Decimal = 0

            If chkcfdid.Checked = True Then
                saldo = lblSaldoAnterior.Text - monto
            Else
                saldo = lblSaldoAnterior.Text
            End If

            Dim dr As DataRow = Session("TmpDetalleComplemento").NewRow()
            dr.Item("id") = detalleid.ToString
            dr.Item("fecha") = lblfechaCFDI.Text
            dr.Item("serie") = lblSerie.Text
            dr.Item("folio") = lblFolio.Text
            dr.Item("monedaFactura") = lblMonedaFactura.Text
            dr.Item("uuid") = lblUUID.Text
            dr.Item("total") = lbltotalCFDI.Text
            dr.Item("saldo") = saldo.ToString
            dr.Item("saldoanterior") = lblSaldoAnterior.Text
            dr.Item("monto") = txtMonto.Text
            dr.Item("montomxn") = txtImportePesos.Text
            dr.Item("chkcfdid") = chkcfdid.Checked
            Session("TmpDetalleComplemento").Rows.Add(dr)
        Next

        itemsList.DataSource = Session("TmpDetalleComplemento")
        itemsList.DataBind()

        CargaTotalCFDI()
        panelResume.Visible = True
    End Sub

    Private Sub ValidarExistComplemento()
        If Session("CFD") = 0 Then
            GetCFD()
        End If

        If Session("PAGOCFD") = 0 Then
            GetPAGOCFD()
        End If
    End Sub

    Protected Sub GetCFD()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=1, @clienteid='" & cmbCliente.SelectedValue & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                Session("CFD") = rs("cfdid")

            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Protected Sub GetPAGOCFD()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        If fecha.SelectedDate Is Nothing Then
            fecha.SelectedDate = Now()
        End If

        If cmbFormaPago.SelectedValue = "01" Then
            txtNumOperacion.Text = "01"
        End If

        Dim fechaP As String = fecha.SelectedDate.Value.ToShortDateString & " " & "12:00:00"

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pComplementoDePago @cmd=2, @clienteid='" & cmbCliente.SelectedValue & "', @formapagoid='" & cmbFormaPago.SelectedValue & "', @numoperacion='" & txtNumOperacion.Text & "', @ctaordenante='" & cmbCtaOrdenante.SelectedItem.Text & "', @rfcemisorctaord='" & txtRFCCtaOrdenante.Text & "', @ctabeneficiaria='" & cmbCtaBeneficiario.SelectedItem.Text & "', @rfcemisorctabeneficiaria='" & txtRfcBeneficiario.Text & "', @nomBancoOrdext='" & txtBancoExtr.Text & "', @fecha_pago='" & fechaP.ToString & "'", conn)

        Dim moneda As String = ""

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Session("PAGOCFD") = rs("pagoid")
            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub CargaTotalCFDI()
        Dim monedaPago As String
        Dim tmpImporte As Decimal = 0
        Dim monedaid As Integer = 0
        Dim moneda As String = ""

        If cmbMoneda.SelectedValue >= 2 Then
            monedaPago = "USD"
        Else
            monedaPago = "MXN"
        End If


        For Each dataItem As Telerik.Web.UI.GridDataItem In itemsList.MasterTableView.Items
            Dim txtMonto As RadNumericTextBox = DirectCast(dataItem.FindControl("txtMonto"), RadNumericTextBox)
            Dim txtImportePesos As RadNumericTextBox = DirectCast(dataItem.FindControl("txtImportePesos"), RadNumericTextBox)
            Dim lblMonedaFactura As Label = DirectCast(dataItem.FindControl("lblMonedaFactura"), Label)
            Dim chkcfdid As System.Web.UI.WebControls.CheckBox = DirectCast(dataItem.FindControl("chkcfdid"), System.Web.UI.WebControls.CheckBox)
            Dim saldo As Decimal = 0

            If chkcfdid.Checked = True Then
                If monedaPago <> lblMonedaFactura.Text Then
                    saldo = txtImportePesos.Text
                Else
                    saldo = txtMonto.Text
                End If
            Else
                saldo = 0
            End If

            If total > 0 Then
                total = total + saldo
            Else
                total = saldo
            End If
        Next

        lblTotalValue.Text = FormatCurrency(total, 2).ToString

    End Sub

    Public Sub txtMonto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim valido As Boolean
        valido = ValidarCFDI()

        If valido = True Then
            CargarSaldoPendiente()
            RadWindow1.VisibleOnPageLoad = False
        End If
    End Sub

    Function ValidarCFDI()
        Dim Validar As Boolean = False
        For Each dataItem As Telerik.Web.UI.GridDataItem In itemsList.MasterTableView.Items
            Dim chkcfdid As System.Web.UI.WebControls.CheckBox = DirectCast(dataItem.FindControl("chkcfdid"), System.Web.UI.WebControls.CheckBox)

            If chkcfdid.Checked = True Then
                Validar = True
            End If
        Next
        Return Validar
    End Function

    Private Sub cmbFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFormaPago.SelectedIndexChanged
        If cmbFormaPago.SelectedValue.Length > 0 Then
            If cmbFormaPago.SelectedValue = "02" Or cmbFormaPago.SelectedValue = "03" Or cmbFormaPago.SelectedValue = "04" Or cmbFormaPago.SelectedValue = "05" Or cmbFormaPago.SelectedValue = "06" Or cmbFormaPago.SelectedValue = "08" Or cmbFormaPago.SelectedValue = "17" Or cmbFormaPago.SelectedValue = "28" Or cmbFormaPago.SelectedValue = "29" Then
                panelRecepcionPago.Visible = True
                If cmbCtaOrdenante.SelectedValue > 0 Then
                    Call CtaExtranjero()
                End If
            Else
                ClearClienteProveedor()
                panelRecepcionPago.Visible = False
            End If
            Validadores()
        End If
    End Sub

    Private Sub CtaExtranjero()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=7, @id='" & cmbCtaOrdenante.SelectedValue & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read() Then
                txtBancoExtr.Text = rs("bancoextranjero")
            End If
            '
            rs.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub ClearClienteProveedor()
        txtRfcBeneficiario.Text = ""
        cmbCtaBeneficiario.SelectedValue = 0
        txtRFCCtaOrdenante.Text = ""
        cmbCtaOrdenante.SelectedValue = 0
        txtBancoExtr.Text = ""
        txtNumOperacion.Text = ""
        cmbTipoPago.SelectedValue = 0
    End Sub

    Private Sub cmbCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCliente.SelectedIndexChanged
        Call CargaCliente(cmbCliente.SelectedValue)
        Call DisplayItems()
        panelItemsRegistration.Visible = True
        itemsList.Visible = True
    End Sub

    Private Sub itemsList_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles itemsList.NeedDataSource
        itemsList.DataSource = Session("TmpDetalleComplemento")
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Response.Redirect("~/portalcfd/ComplementosEmitidos.aspx")
    End Sub

    Private Sub btnCancelInvoice_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelInvoice.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pCFD @cmd=31, @cfdid='" & Session("CFD").ToString & "'")
        ObjData.RunSQLQuery("exec pComplementoDePago @cmd=9, @pagoid='" & Session("PAGOCFD").ToString & "'")
        ObjData = Nothing
        '
        Session("CFD") = 0
        Session("PAGOCFD") = 0
        '
        Response.Redirect("~/portalcfd/ComplementosEmitidos.aspx")
        '
    End Sub

    Private Sub btnCreateInvoice_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateInvoice.Click
        If Page.IsValid Then
            Dim Timbrado As Boolean = False
            Dim MensageError As String = ""
            RadWindow1.VisibleOnPageLoad = False
            '
            Call AgregaCFDI()
            '
            Call CargaTotalCFDI()
            '
            '   Guadar Metodo de Pago
            '
            Call GuadarMetodoPago()
            '
            '   Rutina de generación de XML CFDI Versión 3.3
            '
            m_xmlDOM = CrearDOM()
            '
            '   Verifica que tipo de comprobante se va a emitir
            '
            Call AsignaSerieFolio()

            Comprobante = CrearNodoComprobante()

            m_xmlDOM.AppendChild(Comprobante)
            IndentarNodo(Comprobante, 1)
            '
            '   Agrega los datos del emisor
            '
            Call ConfiguraEmisor()
            '
            '   Asigna los datos del receptor
            '
            Call ConfiguraReceptor()
            '
            '   Agrega los conceptos de la factura
            '
            CrearNodoConceptos(Comprobante)
            IndentarNodo(Comprobante, 1)
            '
            CrearNodoPagos(Comprobante)
            IndentarNodo(Comprobante, 1)
            '
            SellarCFD(Comprobante)
            m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
            m_xmlDOM.Save(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "link_" & serie.Value & folio.Value & ".xml")
            '
            '   Realiza Timbrado
            '
            If folio.Value > 0 Then
                '
                '   Timbrado SIFEI
                '
                Dim Usuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                Dim Password As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                Dim IdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")

                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)

                'Pruebas
                Dim TimbreSifeiVersion33 As New SIFEIPruebas.SIFEIService()

                'Produccion
                'Dim TimbreSifeiVersion33 As New SIFEI40.SIFEIService()

                Call Comprimir()

                Try
                    Dim bytes() As Byte
                    bytes = TimbreSifeiVersion33.getCFDI(Usuario, Password, data, "", IdEquipo)
                    Descomprimir(bytes)
                    Timbrado = True
                Catch ex As SoapException
                    Call cfdnotimbrado()
                    Timbrado = False
                    MensageError = ex.Message.ToString
                Catch ex As FaultException
                    Call cfdnotimbrado()
                    Timbrado = False
                    MensageError = ex.Message.ToString
                End Try

                If Timbrado = True Then
                    '
                    UUID = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "link_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                    '
                    '   Marca el cfd como timbrado
                    '
                    Call cfdtimbrado(UUID)
                    '
                    cadOrigComp = CadenaOriginalComplemento(serie.Value, folio.Value)
                    '
                    '   Genera Código Bidimensional
                    '
                    Call generacbb()
                    '
                    '   Genera PDF
                    '
                    If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\link_" & serie.Value & folio.Value & ".pdf") Then
                        GuardaPDF(GeneraPDF(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\link_" & serie.Value & folio.Value & ".pdf")
                    End If
                    '
                End If
            Else
                MensageError = "No se encontraron folios disponibles."
            End If
            '
            '   Timbrado FactureHoy
            '
            'If folio.Value > 0 Then
            '    '
            '    '   Timbrado FactureHoy
            '    '
            '    Dim queusuariocertus As String = System.Configuration.ConfigurationManager.AppSettings("FactureHoyUsuario")
            '    Dim quepasscertus As String = System.Configuration.ConfigurationManager.AppSettings("FactureHoyContrasena")
            '    Dim queproceso As Integer = System.Configuration.ConfigurationManager.AppSettings("FactureHoyProceso")

            '    Dim MemStream As System.IO.MemoryStream = FileToMemory(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "link_" & serie.Value & folio.Value & ".xml")
            '    Dim archivo As Byte() = MemStream.ToArray()

            '    'Dim service As New FactureHoypruebaV33.WsEmisionTimbrado33()
            '    Dim service As New FactureHoyNT33.WsEmisionTimbrado33Client
            '    Dim puerto = service.EmitirTimbrar(queusuariocertus, quepasscertus, queproceso, archivo)

            '    If Not puerto.XML Is Nothing Then
            '        If puerto.isError Then
            '            '
            '            '   Marca el cfd como no timbrado
            '            '
            '            Call cfdnotimbrado()
            '            '
            '            Response.Write(puerto.message.Trim)
            '            Response.End()
            '        Else
            '            Timbrado = True
            '            MensageError = ""

            '            File.WriteAllBytes(Server.MapPath("~/portalcfd/cfd_storage/") & serie.Value & folio.Value & ".xml", puerto.XML)
            '            cadOrigComp = puerto.cadenaOriginalTimbre
            '            '
            '            '   Genera Código Bidimensional
            '            '
            '            Call generacbb()
            '            '
            '            '   Obtiene el UUID
            '            '
            '            Dim filePath As String = Server.MapPath("~/portalcfd/cfd_storage/") & serie.Value & folio.Value & ".xml"
            '            Dim UUID() As String
            '            ReDim UUID(0)
            '            '
            '            Dim FlujoReader As XmlTextReader = Nothing
            '            Dim j As Integer
            '            FlujoReader = New XmlTextReader(filePath)
            '            FlujoReader.WhitespaceHandling = WhitespaceHandling.None
            '            While FlujoReader.Read()
            '                Select Case FlujoReader.NodeType
            '                    Case XmlNodeType.Element
            '                        If FlujoReader.Name = "tfd:TimbreFiscalDigital" Then
            '                            For j = 0 To FlujoReader.AttributeCount - 1
            '                                FlujoReader.MoveToAttribute(j)
            '                                If FlujoReader.Name = "UUID" Then
            '                                    UUID(0) = FlujoReader.Value.ToString
            '                                End If
            '                            Next
            '                        End If
            '                End Select
            '            End While
            '            '
            '            '   Marca el cfd como timbrado
            '            '
            '            Call cfdtimbrado(UUID(0))
            '            '
            '            '   Genera PDF
            '            '
            '            If Not File.Exists(Server.MapPath("~/portalcfd/pdf/") & serie.Value & folio.Value & ".pdf") Then
            '                GuardaPDF(GeneraPDF(Session("CFD")), Server.MapPath("~/portalcfd/pdf/") & serie.Value & folio.Value & ".pdf")
            '            End If
            '        End If
            '    Else
            '        Call cfdnotimbrado()
            '        Timbrado = False
            '        MensageError = puerto.message.Trim
            '    End If

            '    Session("CFD") = 0
            '    Session("PAGOCFD") = 0

            'Else
            '    MensageError = "No se encontraron folios disponibles."
            'End If

            Session("CFD") = 0
            Session("PAGOCFD") = 0

            If Timbrado = True Then
                Response.Redirect("~/portalcfd/ComplementosEmitidos.aspx")
            Else
                txtErrores.Text = MensageError.ToString
                RadWindow1.VisibleOnPageLoad = True
            End If

        End If
    End Sub

    Private Function Comprimir()
        Dim zip As ZipFile = New ZipFile(serie.Value.ToString & folio.Value.ToString & ".zip")
        zip.AddFile(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "link_" & serie.Value & folio.Value & ".xml", "")
        Dim ms As New MemoryStream()
        zip.Save(ms)
        data = ms.ToArray
    End Function

    Private Function Descomprimir(ByVal data5 As Byte())
        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = Server.MapPath("~/portalcfd/cfd_storage/").ToString
        Dim e As ZipEntry
        For Each e In zip1
            archivo = e.FileName.ToString
            e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
        Next

        Dim Path = Server.MapPath("~/portalcfd/cfd_storage/")
        If File.Exists(Path & archivo) Then
            System.IO.File.Copy(Path & archivo, Path & serie.Value.ToString & folio.Value.ToString & ".xml")
        End If

        response.write(archivo)

    End Function

    Private Function CadenaOriginalComplemento(ByVal serie As String, ByVal folio As Long) As String
        '
        '   Obtiene los valores del timbre de respuesta
        '
        Dim selloSAT As String = ""
        Dim noCertificadoSAT As String = ""
        Dim selloCFD As String = ""
        Dim fechaTimbrado As String = ""
        Dim UUID As String = ""
        Dim Version As String = ""
        Dim RfcProvCertif As String = ""

        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        FlujoReader = New XmlTextReader(Server.MapPath("~/portalcfd/cfd_storage/") & serie.ToString & folio.ToString & ".xml")
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        While FlujoReader.Read()
            Select Case FlujoReader.NodeType
                Case XmlNodeType.Element
                    If FlujoReader.Name = "tfd:TimbreFiscalDigital" Then
                        For i = 0 To FlujoReader.AttributeCount - 1
                            FlujoReader.MoveToAttribute(i)
                            If FlujoReader.Name = "fechaTimbrado" Or FlujoReader.Name = "FechaTimbrado" Then
                                fechaTimbrado = FlujoReader.Value
                            ElseIf FlujoReader.Name = "UUID" Then
                                UUID = FlujoReader.Value
                            ElseIf FlujoReader.Name = "NoCertificadoSAT" Then
                                noCertificadoSAT = FlujoReader.Value
                            ElseIf FlujoReader.Name = "SelloCFD" Then
                                selloCFD = FlujoReader.Value
                            ElseIf FlujoReader.Name = "SelloSAT" Then
                                selloSAT = FlujoReader.Value
                            ElseIf FlujoReader.Name = "Version" Then
                                Version = FlujoReader.Value
                            ElseIf FlujoReader.Name = "RfcProvCertif" Then
                                RfcProvCertif = FlujoReader.Value
                            End If
                        Next
                    End If
            End Select
        End While

        Dim cadena As String = ""
        cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & RfcProvCertif & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        Return cadena

    End Function

    Protected Sub AgregaCFDI()

        Dim MonedaP As String
        If cmbMoneda.SelectedValue = 2 Then
            MonedaP = "USD"
        Else
            MonedaP = "MXN"
        End If

        Dim ObjData As New DataControl()
        For Each rows As DataRow In Session("TmpDetalleComplemento").Rows
            If rows("chkcfdid") = 1 Then
                ObjData.RunSQLQuery("EXEC pComplementoDePago @cmd=3, @complementoId='" & Session("PAGOCFD").ToString & "', @cfdid='" & rows("id") & "', @metodopagoid='PPD', @saldoInsoluto='" & rows("saldo") & "', @monto='" & rows("monto") & "', @montomxn='" & rows("montomxn") & "', @uuid='" & rows("uuid") & "', @monedap='" & MonedaP.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "'")
            End If
        Next
        ObjData = Nothing

    End Sub

    Private Sub GuadarMetodoPago()
        Dim usoCFDIID = "P01"
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=25, @metodopagoid='PPD', @usocfdi='" & usoCFDIID.ToString & "', @serieid='24', @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Function CargaLugarExpedicionAtributos() As String
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=3", conn)
        Dim LugarExpedicion As String = ""
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read() Then
                LugarExpedicion = rs("fac_cp")
            End If

            rs.Close()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        '
        Return LugarExpedicion
        ''
    End Function

    Private Function LeerCertificado() As String
        Dim Certificado As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Certificado = rs("archivo_certificado")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Certificado

    End Function

    Private Function Leerllave() As String
        Dim llave As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                llave = rs("archivo_llave_privada")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return llave
    End Function

    Private Function LeerClave() As String
        Dim contrasena As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                contrasena = rs("contrasena_llave_privada")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return contrasena
    End Function

    Private Sub AsignaSerieFolio()
        '
        '   Obtiene serie y folio
        '
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""
        Dim condicionesid As Integer = 0
        Dim NumCtaPago As String = ""
        Dim nombreaduana As String = ""
        Dim instrucciones As String = ""

        Dim SQLUpdate As String = ""

        SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='24', @instrucciones='" & instrucciones.ToString & "', @aduana='" & nombreaduana.ToString & "', @fecha_pedimento='', @formapagoid='" & cmbFormaPago.SelectedValue.ToString & "', @tipopagoId='PPD', @numctapago='" & NumCtaPago.ToString & "', @condicionesid='" & condicionesid.ToString & "'"

        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand(SQLUpdate, connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie.Value = rs("serie").ToString
                folio.Value = rs("folio").ToString
                aprobacion = rs("aprobacion").ToString
                annioaprobacion = rs("annio_solicitud").ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.End()
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
    End Sub

    Private Sub ConfiguraEmisor()
        '
        '   Datos del Emisor
        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=11", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                CrearNodoEmisor(Comprobante, rs("razonsocial"), rs("fac_rfc"), rs("regimenid"))
                IndentarNodo(Comprobante, 1)
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
        End Try
        ''
    End Sub

    Private Sub ConfiguraReceptor()
        '
        '   Obtiene datos del receptor
        '
        Dim connR As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdR As New SqlCommand("exec pCFD @cmd=12, @clienteId='" & cmbCliente.SelectedValue.ToString & "'", connR)
        Try

            connR.Open()

            Dim rs As SqlDataReader
            rs = cmdR.ExecuteReader()

            If rs.Read Then
                'CrearNodoReceptor(Comprobante, rs("razonsocial"), rs("fac_rfc"), "P01")
                CrearNodoReceptor(Comprobante, rs("denominacion_razon_social"), rs("fac_rfc"), "CP01", rs("fac_cp"), "", "", rs("regimenfiscalid"))
                IndentarNodo(Comprobante, 1)
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            connR.Close()
            connR.Dispose()
            connR = Nothing
        End Try
    End Sub

    Private Sub CrearNodoConceptos(ByVal Nodo As XmlNode)
        Dim Conceptos As XmlElement
        Dim Concepto As XmlElement

        Conceptos = CrearNodo("cfdi:Conceptos")
        IndentarNodo(Conceptos, 2)

        Concepto = CrearNodo("cfdi:Concepto")
        Concepto.SetAttribute("ClaveProdServ", "84111506")
        Concepto.SetAttribute("Cantidad", "1")
        Concepto.SetAttribute("ClaveUnidad", "ACT")
        Concepto.SetAttribute("Descripcion", "Pago")
        Concepto.SetAttribute("ValorUnitario", "0")
        Concepto.SetAttribute("Importe", "0")
        Concepto.SetAttribute("ObjetoImp", "01")
        Conceptos.AppendChild(Concepto)
        IndentarNodo(Conceptos, 1)
        Concepto = Nothing
        Nodo.AppendChild(Conceptos)
    End Sub

    'Private Sub CrearNodoPagos(ByVal Nodo As XmlNode)
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

    '    Dim Totales As XmlElement
    '    Dim Complemento As XmlElement
    '    Complemento = CrearNodo("cfdi:Complemento")
    '    IndentarNodo(Complemento, 1)

    '    Url = 1
    '    Dim Pagos As XmlElement
    '    Dim Pago As XmlElement
    '    Dim DocumentoRelacionado As XmlElement

    '    Pagos = CrearNodo("pago20:Pagos")
    '    Pagos.SetAttribute("Version", "2.0")

    '    Totales = CrearNodo("pago20:Totales")
    '    Totales.SetAttribute("MontoTotalPagos", Format(total, "#0.00"))

    '    IndentarNodo(Pagos, 2)

    '    Pago = CrearNodo("pago20:Pago")

    '    Dim ObjDat As New DataControl
    '    Dim fechaP As DateTime = ObjDat.RunSQLScalarQueryString("select top 1 CONVERT(dateTime,fecha_pago) from tblComplementoPagos where id='" & Session("PAGOCFD").ToString & "'")
    '    ObjDat = Nothing

    '    fechaP = fechaP.ToString("dd/MM/yyyy hh:mm:ss")
    '    calFecha.SelectedDate = fechaP

    '    Pago.SetAttribute("FechaPago", Format(calFecha.SelectedDate, "yyyy-MM-ddTHH:mm:ss"))

    '    Dim Moneda As String

    '    If cmbMoneda.SelectedValue = 1 Then
    '        Moneda = "MXN"
    '    Else
    '        Moneda = "USD"
    '    End If

    '    Pago.SetAttribute("FormaDePagoP", cmbFormaPago.SelectedValue)
    '    Pago.SetAttribute("MonedaP", Moneda.ToString)
    '    Pago.SetAttribute("Monto", Format(total, "#0.00"))

    '    If cmbMoneda.SelectedValue > 1 Then
    '        Pago.SetAttribute("TipoCambioP", FormatNumber(txtTipoCambio.Text, 2).ToString)
    '    Else
    '        Pago.SetAttribute("TipoCambioP", 1)
    '    End If

    '    Dim ObjComp As New DataControl
    '    ObjComp.RunSQLQuery("exec pComplementoDePago @cmd=8, @formapagoid='" & cmbFormaPago.SelectedValue & "', @monedaid='" & cmbMoneda.SelectedValue & "', @tipocambio='" & txtTipoCambio.Text & "', @pagoid='" & Session("PAGOCFD").ToString & "'")
    '    ObjComp = Nothing

    '    If panelRecepcionPago.Visible = True Then
    '        Pago.SetAttribute("NumOperacion", txtNumOperacion.Text)
    '        Pago.SetAttribute("RfcEmisorCtaOrd", txtRFCCtaOrdenante.Text)
    '        Pago.SetAttribute("RfcEmisorCtaBen", txtRfcBeneficiario.Text)
    '        Pago.SetAttribute("CtaOrdenante", cmbCtaOrdenante.SelectedItem.Text)
    '        Pago.SetAttribute("CtaBeneficiario", cmbCtaBeneficiario.SelectedItem.Text)

    '        If txtBancoExtr.Text.Length > 1 Then
    '            Pago.SetAttribute("NomBancoOrdExt", txtBancoExtr.Text)
    '        End If
    '    End If

    '    Dim CertPago As String
    '    Dim CadPago As String
    '    Dim SelloPago As String
    '    Dim tipocadpago As String

    '    If cmbTipoPago.SelectedValue > 0 Then
    '        Pago.SetAttribute("TipoCadPago", "01")
    '        tipocadpago = "01"
    '        Pago.SetAttribute("CertPago", txtCertPago.Text)
    '        CertPago = txtCertPago.Text
    '        Pago.SetAttribute("CadPago", txtCadPago.Text)
    '        CadPago = txtCadPago.Text
    '        Pago.SetAttribute("SelloPago", txtSelloPago.Text)
    '        SelloPago = txtSelloPago.Text

    '        Dim Objdata As New DataControl
    '        Objdata.RunSQLQuery("exec pComplementoDePago @cmd=6, @tipocadpago='" & tipocadpago.ToString & "', @certpago='" & CertPago.ToString & "', @cadpago='" & CadPago.ToString & "', @sellopago='" & SelloPago.ToString & "', @pagoid='" & Session("PAGOCFD").ToString & "'")
    '        Objdata = Nothing

    '    End If

    '    IndentarNodo(Pago, 2)

    '    Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
    '    Dim cmdP As New SqlCommand("exec pComplementoDePago @cmd=4, @pagoid='" & Session("PAGOCFD").ToString & "'", connP)
    '    Try
    '        connP.Open()
    '        '
    '        Dim rs As SqlDataReader
    '        rs = cmdP.ExecuteReader()
    '        '
    '        Dim Monedacfdid As String = ""
    '        Dim saldoAnterior As Decimal = 0
    '        Dim importePagado As Decimal = 0
    '        Dim Saldoinsoluto As Decimal = 0
    '        While rs.Read
    '            DocumentoRelacionado = CrearNodo("pago20:DoctoRelacionado")
    '            DocumentoRelacionado.SetAttribute("IdDocumento", rs("uuid"))
    '            DocumentoRelacionado.SetAttribute("MonedaDR", rs("moneda"))
    '            DocumentoRelacionado.SetAttribute("EquivalenciaDR", "1")

    '            If rs("moneda").ToString = "USD" Or rs("moneda").ToString = "EUR" Then
    '                If rs("tipocambio") > 0 Then
    '                    DocumentoRelacionado.SetAttribute("TipoCambioDR", Format(rs("tipocambio"), "#0.00"))
    '                End If
    '            End If

    '            saldoAnterior = rs("saldoAnterior")
    '            importePagado = rs("importePagado")
    '            Saldoinsoluto = rs("Saldoinsoluto")

    '            'DocumentoRelacionado.SetAttribute("MetodoDePagoDR", rs("metodopagoid"))
    '            DocumentoRelacionado.SetAttribute("NumParcialidad", rs("parcialidad"))
    '            DocumentoRelacionado.SetAttribute("ImpSaldoAnt", Format(saldoAnterior, "#0.00"))
    '            DocumentoRelacionado.SetAttribute("ImpPagado", Format(importePagado, "#0.00"))
    '            DocumentoRelacionado.SetAttribute("ImpSaldoInsoluto", Format(Saldoinsoluto, "#0.00"))
    '            DocumentoRelacionado.SetAttribute("ObjetoImpDR", "01")
    '            Pago.AppendChild(DocumentoRelacionado)
    '            IndentarNodo(Pago, 3)
    '            DocumentoRelacionado = Nothing
    '        End While
    '    Catch ex As Exception
    '        Response.Write(ex.Message.ToString)
    '        Response.End()
    '    Finally
    '        connP.Close()
    '        connP.Dispose()
    '        connP = Nothing
    '    End Try
    '    Pagos.AppendChild(Totales)
    '    Pagos.AppendChild(Pago)
    '    IndentarNodo(Pagos, 1)
    '    Complemento.AppendChild(Pagos)
    '    IndentarNodo(Complemento, 1)
    '    Nodo.AppendChild(Complemento)
    '    Url = 0
    '    '
    '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
    '    Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    '    '
    'End Sub

    Private Sub CrearNodoPagos(ByVal Nodo As XmlNode)

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

        Dim Complemento As XmlElement
        Complemento = CrearNodo("cfdi:Complemento")
        IndentarNodo(Complemento, 1)

        Url = 1
        Dim Pagos As XmlElement
        Dim Pago As XmlElement
        Dim ImpuestosDR As XmlElement
        Dim TrasladosDR As XmlElement
        Dim TrasladoDR As XmlElement
        Dim ImpuestosP As XmlElement
        Dim TrasladosP As XmlElement
        Dim TrasladoP As XmlElement
        Dim Totales As XmlElement
        Dim DocumentoRelacionado As XmlElement

        Dim TotalTrasladosImpuestoIVA16 As Decimal = 0
        Dim TotalTrasladosBaseIVA16 As Decimal = 0
        Dim ImporteP As Decimal = 0
        Dim BaseP As Decimal = 0

        Pagos = CrearNodo("pago20:Pagos")
        Pagos.SetAttribute("Version", "2.0")
        IndentarNodo(Pagos, 2)

        Dim TipoCambioP As Decimal = 0
        Dim MontoTotalPagos As Decimal = 0
        Dim tipocambio As Decimal = 0
        Dim TC_Calculado As Decimal = 0

        Try
            tipocambio = Convert.ToDecimal(txtTipoCambio.Text)
        Catch ex As Exception
            tipocambio = 0
        End Try

        Pago = CrearNodo("pago20:Pago")

        Dim fechaP As String = fecha.SelectedDate.Value.ToShortDateString & " " & "12:00:00"

        Dim ObjComp As New DataControl
        ObjComp.RunSQLQuery("exec pComplementoDePago @cmd=8, @formapagoid='" & cmbFormaPago.SelectedValue & "', @monedaid='" & cmbMoneda.SelectedValue & "', @tipocambio='" & tipocambio.ToString & "', @numoperacion='" & txtNumOperacion.Text & "', @fecha_pago='" & fechaP.ToString & "', @ctaordenante='" & cmbCtaOrdenante.SelectedItem.Text & "', @rfcemisorctaord='" & txtRFCCtaOrdenante.Text & "', @ctabeneficiaria='" & cmbCtaBeneficiario.SelectedItem.Text & "', @rfcemisorctabeneficiaria='" & txtRfcBeneficiario.Text & "', @nomBancoOrdext='" & txtBancoExtr.Text & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "', @pagoid='" & Session("PAGOCFD").ToString & "'")

        calFecha.SelectedDate = fechaP

        Pago.SetAttribute("FechaPago", Format(calFecha.SelectedDate, "yyyy-MM-ddTHH:mm:ss"))

        Dim MonedaP As String = ""

        If cmbMoneda.SelectedValue = 1 Then
            MonedaP = "MXN"
        ElseIf cmbMoneda.SelectedValue = 2 Then
            MonedaP = "USD"
        End If

        If cmbMoneda.SelectedValue > 1 Then
            TipoCambioP = tipocambio
            Pago.SetAttribute("TipoCambioP", Format(tipocambio, "#0.000000"))
        Else
            TipoCambioP = 1
            Pago.SetAttribute("TipoCambioP", "1")
        End If

        Dim ImpPagado As Decimal = 0
        Dim ImpPagadoUSD As Decimal = 0
        Dim Monto As Decimal = 0
        Dim ds As New DataSet
        ds = ObjComp.FillDataSet("exec pComplementoDePago @cmd=4, @pagoid='" & Session("PAGOCFD").ToString & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                If row("monedacfdi").ToString <> row("moneda").ToString Then
                    If row("monedacfdi") = "MXN" And MonedaP <> "MXN" Then
                        If row("monedacfdi") = "MXN" And MonedaP = "USD" Then

                            'Try
                            '    tipocambio = 1 / Convert.ToDecimal(txtTipoCambio.Text)
                            'Catch ex As Exception
                            '    tipocambio = 0
                            'End Try

                            'ImpPagado = row("importeMxn") * tipocambio
                            'Monto = Monto + CDbl(ImpPagado)

                            ImpPagadoUSD = ImpPagadoUSD + row("importePagado")

                            ImpPagado = row("importeMxn")
                            Monto = Monto + CDbl(ImpPagado)

                            TC_Calculado = Math.Round((ImpPagadoUSD / Monto), 6)
                        End If
                    ElseIf row("monedacfdi") = "USD" And MonedaP = "MXN" Then

                        Dim baseDR As Decimal = 0
                        Dim TasaOCuotaDR As String = "0.160000"
                        Dim EquivalenciaDR As Decimal = 0
                        Dim ImpPagadoDeUSDaP As Decimal = 0


                        ' Se obtiene el importe pagado en USD
                        ImpPagadoUSD = row("importePagado")

                        'SE AGREGA LA EQUIVALENCIA EN NODO pago20:Pago
                        EquivalenciaDR = Math.Round(ImpPagadoUSD / row("importeMxn"), 10)

                        TC_Calculado = Math.Round((row("importeMxn") / ImpPagadoUSD), 6)

                        baseDR = Math.Round(ImpPagadoUSD / (1 + TasaOCuotaDR), 10)

                        ImpPagadoDeUSDaP = baseDR / EquivalenciaDR


                        Monto = Monto + CDbl(row("importeMxn"))


                    Else
                        ImpPagado = CDbl(row("importePagado"))
                        Monto = Monto + CDbl(ImpPagado)
                    End If
                Else
                    ImpPagado = CDbl(row("importePagado"))
                    Monto = Monto + CDbl(ImpPagado)
                End If
            Next
        End If
        ObjComp = Nothing

        Pago.SetAttribute("FormaDePagoP", cmbFormaPago.SelectedValue)
        Pago.SetAttribute("MonedaP", MonedaP.ToString)
        Pago.SetAttribute("Monto", Format(Monto, "#0.00"))

        MontoTotalPagos = (Monto * TipoCambioP)

        If panelRecepcionPago.Visible = True Then
            If txtNumOperacion.Text.ToString.Length > 0 Then
                Pago.SetAttribute("NumOperacion", txtNumOperacion.Text)
            End If

            If cmbCtaOrdenante.SelectedValue > 0 Then
                Pago.SetAttribute("CtaOrdenante", cmbCtaOrdenante.SelectedItem.Text)
                Pago.SetAttribute("RfcEmisorCtaOrd", txtRFCCtaOrdenante.Text)
            End If

            If cmbCtaBeneficiario.SelectedValue > 0 Then
                Pago.SetAttribute("CtaBeneficiario", cmbCtaBeneficiario.SelectedItem.Text)
                Pago.SetAttribute("RfcEmisorCtaBen", txtRfcBeneficiario.Text)
            End If

            If txtBancoExtr.Text.Length > 1 Then
                Pago.SetAttribute("NomBancoOrdExt", txtBancoExtr.Text)
            End If
        End If

        Dim CertPago As String
        Dim CadPago As String
        Dim SelloPago As String
        Dim tipocadpago As String

        If cmbTipoPago.SelectedValue > 0 Then
            Pago.SetAttribute("TipoCadPago", "01")
            tipocadpago = "01"
            Pago.SetAttribute("CertPago", txtCertPago.Text)
            CertPago = txtCertPago.Text
            Pago.SetAttribute("CadPago", txtCadPago.Text)
            CadPago = txtCadPago.Text
            Pago.SetAttribute("SelloPago", txtSelloPago.Text)
            SelloPago = txtSelloPago.Text

            Dim Objdata As New DataControl
            Objdata.RunSQLQuery("exec pComplementoDePago @cmd=6, @tipocadpago='" & tipocadpago.ToString & "', @certpago='" & CertPago.ToString & "', @cadpago='" & CadPago.ToString & "', @sellopago='" & SelloPago.ToString & "', @pagoid='" & Session("PAGOCFD").ToString & "'")
            Objdata = Nothing

        End If

        IndentarNodo(Pago, 2)

        Dim ExisteObjetoImpDR As Boolean = False

        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pComplementoDePago @cmd=4, @pagoid='" & Session("PAGOCFD").ToString & "'", connP)
        Try
            connP.Open()
            '
            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()
            '
            Dim Monedacfdid As String = ""
            Dim saldoAnterior As Decimal = 0
            Dim importePagado As Decimal = 0
            Dim Saldoinsoluto As Decimal = 0

            Dim baseTraslado As Decimal = 0

            While rs.Read
                DocumentoRelacionado = CrearNodo("pago20:DoctoRelacionado")
                If CBool(rs("ObjetoImpDR")) Then

                    ExisteObjetoImpDR = True

                    Dim ImpuestoP As String = "002"
                    Dim TipoFactorP As String = "Tasa"
                    Dim TasaOCuotaP As String = "0.160000"

                    Dim tasaOcuota As Decimal = 0.16

                    Dim ImpuestoDR As String = "002"
                    Dim TipoFactorDR As String = "Tasa"
                    Dim TasaOCuotaDR As String = "0.160000"

                    ImpuestosP = CrearNodo("pago20:ImpuestosP")
                    ImpuestosDR = CrearNodo("pago20:ImpuestosDR")
                    TrasladosDR = CrearNodo("pago20:TrasladosDR")

                    Try
                        tipocambio = Convert.ToDecimal(txtTipoCambio.Text)
                    Catch ex As Exception
                        tipocambio = 0
                    End Try

                    If rs("monedacfdi") = "MXN" And rs("moneda") = "MXN" Then

                        baseTraslado = Math.Round((rs("importePagado") / (1 + tasaOcuota)), 2)

                        ImporteP = ImporteP + Math.Round((baseTraslado * tasaOcuota), 2)
                        BaseP = BaseP + baseTraslado

                        TotalTrasladosImpuestoIVA16 = TotalTrasladosImpuestoIVA16 + Math.Round((baseTraslado * tasaOcuota), 2)
                        TotalTrasladosBaseIVA16 = TotalTrasladosBaseIVA16 + baseTraslado

                    ElseIf rs("monedacfdi") = "USD" And rs("moneda") = "USD" Then

                        baseTraslado = Math.Round((rs("importePagado") / (1 + tasaOcuota)), 2)

                        ImporteP = ImporteP + Math.Round((baseTraslado * tasaOcuota), 2)
                        BaseP = BaseP + baseTraslado

                        TotalTrasladosImpuestoIVA16 = TotalTrasladosImpuestoIVA16 + (Math.Round((baseTraslado * tasaOcuota), 2) * tipocambio)
                        TotalTrasladosBaseIVA16 = TotalTrasladosBaseIVA16 + (Math.Round(baseTraslado, 2) * tipocambio)

                    ElseIf rs("monedacfdi") = "USD" And rs("moneda") = "MXN" Then

                        'baseTraslado = Math.Round((rs("importeMxn") / (1 + tasaOcuota)), 6)
                        baseTraslado = Math.Round(((rs("importeMxn") / TC_Calculado) / (1 + tasaOcuota)), 6)

                        ImporteP = ImporteP + ((baseTraslado * tasaOcuota) * TC_Calculado)
                        BaseP = BaseP + (baseTraslado * TC_Calculado)

                        'ImporteP = ImporteP + Math.Truncate(((baseTraslado * tasaOcuota) * TC_Calculado) * 100) / 100
                        'BaseP = BaseP + Math.Truncate((baseTraslado * TC_Calculado) * 100) / 100


                        TotalTrasladosImpuestoIVA16 = TotalTrasladosImpuestoIVA16 + (((baseTraslado) * tasaOcuota) * TC_Calculado)
                        TotalTrasladosBaseIVA16 = TotalTrasladosBaseIVA16 + (baseTraslado * TC_Calculado)

                        'TotalTrasladosImpuestoIVA16 = TotalTrasladosImpuestoIVA16 + Math.Truncate(((baseTraslado * tasaOcuota) * TC_Calculado) * 100) / 100
                        'TotalTrasladosBaseIVA16 = TotalTrasladosBaseIVA16 + Math.Truncate((baseTraslado * TC_Calculado) * 100) / 100



                    ElseIf rs("monedacfdi") = "MXN" And rs("moneda") = "USD" Then

                        baseTraslado = (rs("importePagado") / (1 + tasaOcuota))

                        ImporteP = ImporteP + ((baseTraslado / TC_Calculado) * tasaOcuota)
                        BaseP = BaseP + (baseTraslado / TC_Calculado)

                        TotalTrasladosImpuestoIVA16 = TotalTrasladosImpuestoIVA16 + ((baseTraslado) * tasaOcuota)
                        TotalTrasladosBaseIVA16 = TotalTrasladosBaseIVA16 + (baseTraslado)
                    Else
                        'TotalTrasladosImpuestoIVA16 = TotalTrasladosImpuestoIVA16 + (row("importe") * tipocambio)
                        'TotalTrasladosBaseIVA16 = TotalTrasladosBaseIVA16 + (baseTraslado * tipocambio)

                        'ImporteP = ImporteP + (row("importe") * tipocambio)
                        'BaseP = BaseP + (baseTraslado * tipocambio)
                    End If

                    TrasladoDR = CrearNodo("pago20:TrasladoDR")
                    'TrasladoDR.SetAttribute("BaseDR", Format(baseTraslado, "#0.00"))
                    If TC_Calculado > 0 Then
                        TrasladoDR.SetAttribute("BaseDR", Format(baseTraslado, "#0.000000"))
                    Else
                        TrasladoDR.SetAttribute("BaseDR", Format(baseTraslado, "#0.00"))
                    End If
                    TrasladoDR.SetAttribute("ImpuestoDR", ImpuestoDR)
                    TrasladoDR.SetAttribute("TipoFactorDR", TipoFactorDR)
                    TrasladoDR.SetAttribute("TasaOCuotaDR", TasaOCuotaDR)
                    'TrasladoDR.SetAttribute("ImporteDR", Format(baseTraslado * tasaOcuota, "#0.00"))
                    If TC_Calculado > 0 Then
                        TrasladoDR.SetAttribute("ImporteDR", Format(baseTraslado * tasaOcuota, "#0.000000"))
                    Else
                        TrasladoDR.SetAttribute("ImporteDR", Format(baseTraslado * tasaOcuota, "#0.00"))
                    End If
                    TrasladosDR.AppendChild(TrasladoDR)
                    ImpuestosDR.AppendChild(TrasladosDR)

                    TrasladosP = CrearNodo("pago20:TrasladosP")
                    TrasladoP = CrearNodo("pago20:TrasladoP")
                    TrasladoP.SetAttribute("BaseP", Format(BaseP, "#0.00"))
                    'If TC_Calculado > 0 Then
                    '    TrasladoP.SetAttribute("BaseP", Format(BaseP, "#0.000000"))
                    'Else
                    '    TrasladoP.SetAttribute("BaseP", Format(BaseP, "#0.00"))
                    'End If
                    TrasladoP.SetAttribute("ImpuestoP", ImpuestoP)
                    TrasladoP.SetAttribute("TipoFactorP", TipoFactorP)
                    TrasladoP.SetAttribute("TasaOCuotaP", TasaOCuotaP)
                    TrasladoP.SetAttribute("ImporteP", Format(ImporteP, "#0.00"))
                    'If TC_Calculado > 0 Then
                    '    TrasladoP.SetAttribute("ImporteP", Format(ImporteP, "#0.000000"))
                    'Else
                    '    TrasladoP.SetAttribute("ImporteP", Format(ImporteP, "#0.00"))
                    'End If
                    TrasladosP.AppendChild(TrasladoP)
                    ImpuestosP.AppendChild(TrasladosP)

                    DocumentoRelacionado.AppendChild(ImpuestosDR)
                End If

                DocumentoRelacionado.SetAttribute("IdDocumento", rs("uuid"))
                DocumentoRelacionado.SetAttribute("MonedaDR", rs("monedacfdi"))

                saldoAnterior = rs("saldoAnterior")
                importePagado = rs("importePagado")
                Saldoinsoluto = rs("Saldoinsoluto")

                If rs("monedacfdi").ToString <> rs("moneda").ToString Then
                    If rs("monedacfdi") = "MXN" And MonedaP <> "MXN" Then
                        If rs("monedacfdi") = "MXN" And MonedaP = "USD" Then

                            Try
                                tipocambio = Convert.ToDecimal(txtTipoCambio.Text)
                            Catch ex As Exception
                                tipocambio = 0
                            End Try

                            importePagado = rs("importeMxn") * tipocambio
                            Saldoinsoluto = rs("saldoAnterior") - importePagado

                            Dim EquivalenciaDR As Decimal = 0
                            EquivalenciaDR = importePagado / rs("importeMxn")

                            Dim DataControl As New DataControl
                            DataControl.RunSQLQuery("exec pComplementoDePago @cmd=12, @importePagado='" & importePagado.ToString & "', @id='" & rs("id").ToString & "'")
                            DataControl.RunSQLQuery("exec pComplementoDePago @cmd=13, @tipocambio='" & tipocambio.ToString & "', @id='" & rs("id").ToString & "'")

                            DocumentoRelacionado.SetAttribute("EquivalenciaDR", Format(EquivalenciaDR, "#0.00000000"))
                            DataControl.RunSQLQuery("update tblDetalleComplementoPagos set EquivalenciaDR='" & Format(EquivalenciaDR, "#0.00000000") & "' where id='" & rs("id").ToString & "'")
                            DataControl = Nothing

                            'Try
                            '    tipocambio = 1 / Convert.ToDecimal(txtTipoCambio.Text)
                            'Catch ex As Exception
                            '    tipocambio = 0
                            'End Try

                            'importePagado = rs("importeMxn") * tipocambio
                            'Saldoinsoluto = rs("saldoAnterior") - importePagado

                            'Dim EquivalenciaDR As Decimal = 0
                            'EquivalenciaDR = importePagado / rs("importeMxn")

                            'Dim DataControl As New DataControl
                            'DataControl.RunSQLQuery("exec pComplementoDePago @cmd=12, @importePagado='" & importePagado.ToString & "', @id='" & rs("id").ToString & "'")
                            'DataControl.RunSQLQuery("exec pComplementoDePago @cmd=13, @tipocambio='" & tipocambio.ToString & "', @id='" & rs("id").ToString & "'")

                            'DocumentoRelacionado.SetAttribute("EquivalenciaDR", Format(EquivalenciaDR, "#0.0000000000"))
                            'DataControl.RunSQLQuery("update tblDetalleComplementoPagos set EquivalenciaDR='" & Format(EquivalenciaDR, "#0.0000000000") & "' where id='" & rs("id").ToString & "'")
                            'DataControl = Nothing
                        End If
                    ElseIf rs("monedacfdi") = "USD" And MonedaP = "MXN" Then

                        Try
                            tipocambio = Convert.ToDecimal(txtTipoCambio.Text)
                        Catch ex As Exception
                            tipocambio = 0
                        End Try

                        Dim EquivalenciaDR As Decimal = 0
                        EquivalenciaDR = importePagado / rs("importeMxn")

                        Dim DataControl As New DataControl
                        DataControl.RunSQLQuery("exec pComplementoDePago @cmd=12, @importePagado='" & importePagado.ToString & "', @id='" & rs("id").ToString & "'")
                        DataControl.RunSQLQuery("exec pComplementoDePago @cmd=13, @tipocambio='" & tipocambio.ToString & "', @id='" & rs("id").ToString & "'")

                        DocumentoRelacionado.SetAttribute("EquivalenciaDR", Format(EquivalenciaDR, "#0.00000000"))
                        DataControl.RunSQLQuery("update tblDetalleComplementoPagos set EquivalenciaDR='" & Format(EquivalenciaDR, "#0.00000000") & "' where id='" & rs("id").ToString & "'")
                        DataControl = Nothing
                    Else
                        If rs("monedacfdi") = MonedaP Then
                            DocumentoRelacionado.SetAttribute("EquivalenciaDR", "1")
                            Dim DataControl As New DataControl
                            DataControl.RunSQLQuery("update tblDetalleComplementoPagos set EquivalenciaDR='1' where id='" & rs("id").ToString & "'")
                            DataControl = Nothing
                        End If
                    End If
                Else
                    If rs("monedacfdi") = MonedaP Then
                        DocumentoRelacionado.SetAttribute("EquivalenciaDR", "1")
                        Dim DataControl As New DataControl
                        DataControl.RunSQLQuery("update tblDetalleComplementoPagos set EquivalenciaDR='1' where id='" & rs("id").ToString & "'")
                        DataControl = Nothing
                    End If
                End If

                DocumentoRelacionado.SetAttribute("NumParcialidad", rs("parcialidad"))
                DocumentoRelacionado.SetAttribute("ImpSaldoAnt", Format(saldoAnterior, "#0.00"))
                DocumentoRelacionado.SetAttribute("ImpPagado", Format(importePagado, "#0.00"))
                DocumentoRelacionado.SetAttribute("ImpSaldoInsoluto", Format(Saldoinsoluto, "#0.00"))

                If CBool(ExisteObjetoImpDR) Then
                    DocumentoRelacionado.SetAttribute("ObjetoImpDR", "02")
                Else
                    DocumentoRelacionado.SetAttribute("ObjetoImpDR", "01")
                End If

                Pago.AppendChild(DocumentoRelacionado)
                IndentarNodo(Pago, 3)
                DocumentoRelacionado = Nothing
            End While
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try

        Totales = CrearNodo("pago20:Totales")
        If TotalTrasladosImpuestoIVA16 > 0 Then
            Totales.SetAttribute("TotalTrasladosImpuestoIVA16", Format(Math.Round(TotalTrasladosImpuestoIVA16, 2, MidpointRounding.AwayFromZero), "#0.00"))
        End If
        If TotalTrasladosBaseIVA16 > 0 Then
            Totales.SetAttribute("TotalTrasladosBaseIVA16", Format(Math.Round(TotalTrasladosBaseIVA16, 2, MidpointRounding.AwayFromZero), "#0.00"))
        End If
        'Totales.SetAttribute("MontoTotalPagos", Format(Math.Round(MontoTotalPagos, 2, MidpointRounding.AwayFromZero), "#0.00"))
        Totales.SetAttribute("MontoTotalPagos", Format(Math.Round(TotalTrasladosImpuestoIVA16 + TotalTrasladosBaseIVA16, 2, MidpointRounding.AwayFromZero), "#0.00"))

        If CBool(ExisteObjetoImpDR) Then
            Pago.AppendChild(ImpuestosP)
        End If

        Pagos.AppendChild(Totales)
        Pagos.AppendChild(Pago)
        IndentarNodo(Pagos, 1)
        Complemento.AppendChild(Pagos)
        IndentarNodo(Complemento, 1)
        Nodo.AppendChild(Complemento)
        Url = 0
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement)
        Dim Certificado As String = ""
        Certificado = LeerCertificado()

        Dim Clave As String = ""
        Clave = LeerClave()

        Dim bRawData As Byte() = ReadFile(Server.MapPath("~/portalcfd/certificados/") & Certificado)
        Dim objCert As New X509Certificate2()
        objCert.Import(bRawData)

        NodoComprobante.SetAttribute("NoCertificado", FormatearSerieCert(objCert.SerialNumber))
        NodoComprobante.SetAttribute("Total", "0")
        NodoComprobante.SetAttribute("Certificado", Convert.ToBase64String(bRawData))
        NodoComprobante.SetAttribute("Sello", GenerarSello(Clave))
    End Sub

    Private Function GenerarSello(ByVal Clave As String) As String
        Try
            Dim pkey As New Chilkat.PrivateKey
            Dim pkeyXml As String
            Dim rsa As New Chilkat.Rsa
            pkey.LoadPkcs8EncryptedFile(Server.MapPath("~/portalcfd/llave/") & Leerllave(), Clave)
            pkeyXml = pkey.GetXml()
            rsa.UnlockComponent("RSAT34MB34N_7F1CD986683M")
            rsa.ImportPrivateKey(pkeyXml)
            rsa.Charset = "utf-8"
            rsa.EncodingMode = "base64"
            rsa.LittleEndian = 0
            Dim base64Sig As String
            base64Sig = rsa.SignStringENC(GetCadenaOriginal(m_xmlDOM.InnerXml), "sha256")
            GenerarSello = base64Sig
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        End Try
    End Function

    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim cadena As String = ""
        Try
            Dim xslt As New XslCompiledTransform
            Dim xmldoc As New XmlDocument
            Dim navigator As XPath.XPathNavigator
            Dim output As New StringWriter
            xmldoc.LoadXml(xmlCFD)
            navigator = xmldoc.CreateNavigator()
            'xslt.Load(Server.MapPath("~/portalcfd/SAT/cadenaoriginal_3_3.xslt"))
            xslt.Load("http://www.sat.gob.mx/sitio_internet/cfd/4/cadenaoriginal_4_0/cadenaoriginal_4_0.xslt")
            xslt.Transform(navigator, Nothing, output)
            cadena = output.ToString
        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        End Try

        Return cadena

    End Function

    Private Function FileToMemory(ByVal Filename As String) As MemoryStream
        Dim FS As New System.IO.FileStream(Filename, FileMode.Open)
        Dim MS As New System.IO.MemoryStream
        Dim BA(FS.Length - 1) As Byte
        FS.Read(BA, 0, BA.Length)
        FS.Close()
        MS.Write(BA, 0, BA.Length)
        Return MS
    End Function

    Private Sub cfdnotimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub generacbb()
        Dim CadenaCodigoBidimensional As String = ""
        Dim FinalSelloDigitalEmisor As String = ""

        Dim UUID As String = ""
        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""
        Dim sello As String = ""
        '
        '   Obtiene datos del cfdi para construir string del CBB
        '
        rfcE = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & serie.Value & folio.Value & ".xml", "Rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & serie.Value & folio.Value & ".xml", "Rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & serie.Value & folio.Value & ".xml", "Total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & serie.Value & folio.Value & ".xml", "UUID", "tfd:TimbreFiscalDigital")
        sello = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & serie.Value & folio.Value & ".xml", "SelloCFD", "tfd:TimbreFiscalDigital")
        FinalSelloDigitalEmisor = Mid(sello, (Len(sello) - 7))
        '
        Dim totalDec As Decimal = CType(total, Decimal)
        '
        CadenaCodigoBidimensional = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & rfcE & "&rr=" & rfcR & "&tt=" & totalDec.ToString & "&fe=" & FinalSelloDigitalEmisor
        '
        '   Genera gráfico
        '
        Dim qrCodeEncoder As QRCodeEncoder = New QRCodeEncoder
        qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE
        qrCodeEncoder.QRCodeScale = 6
        qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L
        'La versión "0" calcula automáticamente el tamaño
        qrCodeEncoder.QRCodeVersion = 0

        qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor)
        qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor)

        Dim CBidimensional As Drawing.Image
        CBidimensional = qrCodeEncoder.Encode(CadenaCodigoBidimensional, System.Text.Encoding.UTF8)
        CBidimensional.Save(Server.MapPath("~/portalCFD/cbb/") & serie.Value & folio.Value & ".png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Private Function Num2Text(ByVal value As Decimal) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

    Private Sub cfdtimbrado(ByVal uuid As String)
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=24, @uuid='" & uuid.ToString & "', @cfdid='" & Session("CFD").ToString & "', @pagoid='" & Session("PAGOCFD").ToString & "'")
        Objdata = Nothing
    End Sub

#Region "Nodos para Crear XML"

    Public Function GetXmlAttribute(ByVal url As String, ByVal campo As String, ByVal nodo As String) As String
        Dim valor As String = ""
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer

        FlujoReader = New XmlTextReader(url)
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        Try
            While FlujoReader.Read()
                Select Case FlujoReader.NodeType
                    Case XmlNodeType.Element
                        If FlujoReader.Name = nodo Then
                            For i = 0 To FlujoReader.AttributeCount - 1
                                FlujoReader.MoveToAttribute(i)
                                If FlujoReader.Name = campo Then
                                    valor = FlujoReader.Value.ToString
                                End If
                            Next
                        End If
                End Select
            End While
        Catch ex As Exception
            valor = ""
        End Try

        Return valor

    End Function

    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function

    Private Function CrearNodo(ByVal Nombre As String) As XmlNode
        If Url = 0 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, URI_SAT)
        ElseIf Url = 1 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, "http://www.sat.gob.mx/Pagos20")
        End If
    End Function

    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub

    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal Regimen As String)
        Dim Emisor As XmlElement
        Emisor = CrearNodo("cfdi:Emisor")
        Emisor.SetAttribute("Nombre", nombre.ToUpper)
        Emisor.SetAttribute("Rfc", rfc)
        Emisor.SetAttribute("RegimenFiscal", Regimen)
        Nodo.AppendChild(Emisor)
    End Sub
    Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal UsoCFDI As String, ByVal DomicilioFiscalReceptor As String, ByVal ResidenciaFiscal As String, ByVal NumRegIdTrib As String, ByVal RegimenFiscalReceptor As String)
        Dim Receptor As XmlElement
        Receptor = CrearNodo("cfdi:Receptor")
        Receptor.SetAttribute("Rfc", rfc)
        Receptor.SetAttribute("Nombre", nombre.ToUpper)
        Receptor.SetAttribute("RegimenFiscalReceptor", RegimenFiscalReceptor)
        If DomicilioFiscalReceptor.Length > 0 Then
            Receptor.SetAttribute("DomicilioFiscalReceptor", DomicilioFiscalReceptor)
        End If
        If ResidenciaFiscal.Length > 0 Then
            Receptor.SetAttribute("ResidenciaFiscal", ResidenciaFiscal)
        End If
        If NumRegIdTrib.Length > 0 Then
            Receptor.SetAttribute("NumRegIdTrib", NumRegIdTrib)
        End If
        Receptor.SetAttribute("UsoCFDI", UsoCFDI)
        Nodo.AppendChild(Receptor)
    End Sub
    'Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal UsoCFDI As String)
    '    Dim Receptor As XmlElement
    '    Receptor = CrearNodo("cfdi:Receptor")
    '    Receptor.SetAttribute("Rfc", rfc)
    '    Receptor.SetAttribute("Nombre", nombre)
    '    Receptor.SetAttribute("UsoCFDI", UsoCFDI)
    '    Nodo.AppendChild(Receptor)
    'End Sub

    Private Function CrearNodoComprobante() As XmlNode
        Dim Comprobante As XmlNode
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante)
        CrearNodoComprobante = Comprobante
    End Function

    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement)
        Nodo.SetAttribute("xmlns:cfdi", URI_SAT)
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Nodo.SetAttribute("xmlns:pago20", "http://www.sat.gob.mx/Pagos20")
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd")
        Nodo.SetAttribute("Version", "4.0")
        If serie.Value <> "" Then
            Nodo.SetAttribute("Serie", serie.Value)
        End If
        Nodo.SetAttribute("Folio", folio.Value)
        Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss"))
        Nodo.SetAttribute("Sello", "")
        Nodo.SetAttribute("NoCertificado", "")
        Nodo.SetAttribute("Certificado", "")
        Nodo.SetAttribute("SubTotal", "0")
        Nodo.SetAttribute("Moneda", "XXX")
        Nodo.SetAttribute("Total", "0")
        Nodo.SetAttribute("TipoDeComprobante", "P")
        Nodo.SetAttribute("LugarExpedicion", CargaLugarExpedicionAtributos())
        Nodo.SetAttribute("Exportacion", "01")
    End Sub

    Function ReadFile(ByVal strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = CInt(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function

    Public Function FormatearSerieCert(ByVal Serie As String) As String
        Dim Resultado As String = ""
        Dim I As Integer
        For I = 2 To Len(Serie) Step 2
            Resultado = Resultado & Mid(Serie, I, 1)
        Next
        FormatearSerieCert = Resultado
    End Function

#End Region

#Region "Manejo de PDF"

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    Private Function GeneraPDF(ByVal cfdid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim plantillaid As Integer = 1
        Dim serie As String = ""
        Dim folio As Integer = 0
        Dim tipoid As Integer = 0
        Dim razonsocial As String = ""
        Dim callenum As String = ""
        Dim colonia As String = ""
        Dim ciudad As String = ""
        Dim rfc As String = ""
        Dim em_razonsocial As String = ""
        Dim em_callenum As String = ""
        Dim em_colonia As String = ""
        Dim em_ciudad As String = ""
        Dim em_rfc As String = ""
        Dim em_regimen As String = ""
        Dim importe As Decimal = 0
        Dim importetasacero As Decimal = 0
        Dim iva As Decimal = 0
        Dim total As Decimal = 0
        Dim CantidadTexto As String = ""
        Dim metodopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""
        Dim uuid As String = ""

        'Información Cliente-Proveedor ********
        Dim nombrebancoctaord As String = ""
        Dim nombrebancobeneficiario As String = ""
        Dim rfcemisorctaord As String = ""
        Dim ctaordenante As String = ""
        Dim rfcemisorctabeneficiario As String = ""
        Dim ctabeneficiario As String = ""
        Dim nomBancoOrdExt As String = ""

        'Información del Depósito ********
        Dim fechaPago As String = ""
        Dim moneda As String = ""
        Dim tipocambio As String = ""
        Dim monto As Decimal = 0
        Dim numoperacion As String = ""

        'SPEI-Digital ********
        Dim tipoCadPago As String = ""
        Dim certPago As String = ""
        Dim cadPago As String = ""
        Dim selloPago As String = ""

        Dim usoCFDI As String = ""
        Dim LugarExpedicion As String = ""
        Dim TipoComprobante As String = ""

        Dim ds As DataSet = New DataSet

        Try
            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                plantillaid = rs("plantillaid")
                serie = rs("serie")
                folio = rs("folio")
                tipoid = rs("tipoid")
                em_razonsocial = rs("em_razonsocial")
                em_callenum = rs("em_callenum")
                em_colonia = rs("em_colonia")
                em_ciudad = rs("em_ciudad")
                em_rfc = rs("em_rfc")
                em_regimen = rs("regimen")
                razonsocial = rs("razonsocial")
                callenum = rs("callenum")
                colonia = rs("colonia")
                ciudad = rs("ciudad")
                rfc = rs("rfc")
                importe = rs("importe")
                importetasacero = rs("importetasacero")
                iva = rs("iva")
                total = rs("total")
                monto = rs("monto")
                fechaPago = rs("fechapago")
                tipocambio = rs("tipocambio")
                moneda = rs("moneda")
                metodopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
                usoCFDI = rs("usocfdi")
                uuid = rs("uuid")
                '
                nombrebancoctaord = rs("nomBancoctaord")
                nombrebancobeneficiario = rs("nomBancobeneficiario")
                rfcemisorctaord = rs("rfcemisorctaord")
                ctaordenante = rs("ctaordenante")
                rfcemisorctabeneficiario = rs("rfcemisorctabeneficiaria")
                ctabeneficiario = rs("ctabeneficiaria")
                nomBancoOrdExt = rs("nomBancoOrdext")
                numoperacion = rs("numoperacion")
                '
                tipoCadPago = rs("tipocadpago")
                certPago = rs("certpago")
                cadPago = rs("cadpago")
                selloPago = rs("sellopago")
            End If
            rs.Close()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Dim largo = Len(CStr(Format(CDbl(monto), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(monto), "#,###.00")), largo - 2)

        If moneda = "Pesos (MXN)" Then
            CantidadTexto = "Son " + Num2Text(monto - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 MXN"
        ElseIf moneda = "Dólares (USD)" Then
            CantidadTexto = "Son " + Num2Text(monto - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD"
        End If

        LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "LugarExpedicion", "cfdi:Comprobante")
        TipoComprobante = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "TipoDeComprobante", "cfdi:Comprobante")

        If TipoComprobante.ToString <> "" Then
            Dim ObjData As New DataControl
            TipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & TipoComprobante.ToString & "'")
            ObjData = Nothing
        End If

        If Not File.Exists(Server.MapPath("~/portalcfd/cbb/" & serie & folio.ToString.ToString & ".png")) Then
            Call generacbb()
        End If

        Dim reporte As New Formatos.formato_complemento33iu()
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("txtDocumento").Value = "Pago No.    " & serie.ToString & folio.ToString
        reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "Fecha", "cfdi:Comprobante")
        reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "Nombre", "cfdi:Receptor")
        reporte.ReportParameters("txtClienteCalleNum").Value = callenum
        reporte.ReportParameters("txtClienteColonia").Value = colonia
        reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
        reporte.ReportParameters("txtClienteRFC").Value = "RFC: " & GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "Rfc", "cfdi:Receptor")
        reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "UUID", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "NoCertificado", "cfdi:Comprobante")
        reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "Sello", "cfdi:Comprobante")
        reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "SelloSAT", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
        reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
        reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie & folio.ToString.ToString & ".png")
        reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
        reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
        reporte.ReportParameters("txtMetodoPago").Value = metodopago.ToString
        If numctapago.Length > 0 Then
            reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
        End If
        reporte.ReportParameters("txtUsoCFDI").Value = " USO CFDI: " & usoCFDI.ToString.ToUpper
        '
        '   Complemento Pago
        '
        reporte.ReportParameters("txtCtaordenante").Value = ctaordenante
        reporte.ReportParameters("txtRfcemisorctabeneficiario").Value = rfcemisorctabeneficiario
        reporte.ReportParameters("txtCtabeneficiario").Value = ctabeneficiario
        reporte.ReportParameters("txtRfcemisorctaord").Value = rfcemisorctaord
        reporte.ReportParameters("txtNomBancoOrdExt").Value = nomBancoOrdExt
        reporte.ReportParameters("txtFechaPago").Value = fechaPago
        reporte.ReportParameters("txtMonto").Value = FormatCurrency(monto, 2).ToString
        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        reporte.ReportParameters("txtTipoCambio").Value = FormatCurrency(tipocambio, 2).ToString
        reporte.ReportParameters("txtNumoperacion").Value = numoperacion
        reporte.ReportParameters("txtTipoCadPago").Value = tipoCadPago
        reporte.ReportParameters("txtCertPago").Value = certPago
        reporte.ReportParameters("txtCadPago").Value = cadPago
        reporte.ReportParameters("txtSelloPago").Value = selloPago
        reporte.ReportParameters("txtTipoComprobante").Value = TipoComprobante.ToString
        reporte.ReportParameters("txtPACCertifico").Value = "PAC Certificó: " & GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage/") & serie & folio.ToString & ".xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
        reporte.ReportParameters("txtMoneda").Value = moneda
        reporte.ReportParameters("txtNomBancoOrd").Value = nombrebancoctaord
        reporte.ReportParameters("txtNomBancoBen").Value = nombrebancobeneficiario

        Return reporte

    End Function

#End Region

    Private Sub cmbTipoPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoPago.SelectedIndexChanged
        If cmbTipoPago.SelectedValue <> 0 Then
            panelSPEI.Visible = True
            RequiredFieldValidator7.Enabled = True
            RequiredFieldValidator8.Enabled = True
            RequiredFieldValidator33.Enabled = True
        Else
            panelSPEI.Visible = False
            RequiredFieldValidator7.Enabled = False
            RequiredFieldValidator8.Enabled = False
            RequiredFieldValidator33.Enabled = False
        End If
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Dim valido As Boolean
        valido = ValidarCFDI()

        If valido = True Then
            CargarSaldoPendiente()
            RadWindow1.VisibleOnPageLoad = False
        End If
    End Sub

    Private Sub cmbCtaBeneficiario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCtaBeneficiario.SelectedIndexChanged
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim cmd As New SqlDataAdapter("SELECT isnull(rfc,'') rfc FROM tblCuentasBeneficiario where id = '" + cmbCtaBeneficiario.SelectedValue + "' order by banco", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            txtRfcBeneficiario.Text = ds.Tables(0).Rows(0).Item(0).ToString


            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub cmbCtaOrdenante_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbCtaOrdenante.SelectedIndexChanged
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim cmd As New SqlDataAdapter("SELECT isnull(rfc,'') rfc FROM tblCuentasBancarias where id = '" + cmbCtaOrdenante.SelectedValue + "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            txtRFCCtaOrdenante.Text = ds.Tables(0).Rows(0).Item(0).ToString


            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub cmbMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMoneda.SelectedIndexChanged
        If cmbMoneda.SelectedValue <> 1 Then
            txtTipoCambio.Enabled = True
            valTipoCambio.Enabled = True
        Else
            txtTipoCambio.Enabled = False
            valTipoCambio.Enabled = False
        End If
    End Sub




End Class