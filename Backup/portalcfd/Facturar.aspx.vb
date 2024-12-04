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

Partial Class portalcfd_Facturar
    Inherits System.Web.UI.Page
    Private importe As Decimal = 0
    Private iva As Decimal = 0
    Private ieps As Integer = 0
    Private ieps_total As Decimal = 0
    Private total As Decimal = 0
    Private importesindescuento As Decimal = 0
    Private importetasacero As Decimal = 0
    Private totaldescuento As Decimal = 0
    Private totalieps As Decimal = 0
    Private retencion4 As Decimal = 0
    Private tieneIvaTasaCero As Boolean = False
    Private tieneIva16 As Boolean = False
    Private archivoLlavePrivada As String = ""
    Private contrasenaLlavePrivada As String = ""
    Private archivoCertificado As String = ""
    Private _selloCFD As String = ""
    Private _cadenaOriginal As String = ""

    Private tipocontribuyenteid As Integer = 0
    Private tipoid As Integer = 0
    Private tipoprecioid As Integer
    Private cadOrigComp As String
    '**************************
    Private m_xmlDOM As New XmlDocument
    Const URI_SAT = "http://www.sat.gob.mx/cfd/4"
    Private listErrores As New List(Of String)
    Private Comprobante As XmlNode
    Public Const NOMBRE_XSLT = "cadenaoriginal_3_3.xslt"
    Public Const DIR_SAT = "\SAT\"
    Dim UUID As String = ""
    Dim data As Byte()
    Dim AplicarRetencion As Boolean = False
    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb
    Private AplicaImpuestoTasa0 As Boolean = False
#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsSelectionLegend.Text = Resources.Resource.lblClientsSelectionLegend
            lblClientData.Text = Resources.Resource.lblClientData
            lblClientItems.Text = Resources.Resource.lblItems
            lblResume.Text = Resources.Resource.lblResume

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            Dim ObjCat As New DataControl
            ObjCat.Catalogo(cmbCliente, "EXEC pMisClientes @cmd=1, @clienteUnionId='" & Session("clienteid") & "' ", 0)
            ObjCat.Catalogo(cmbDocumento, "select id, isnull(nombre,'') as nombre from tblTipoDocumento where id in (select distinct tipoid from tblMisFolios where ISNULL(utilizado,0)=0) and id not in(10,11,15,24) order by nombre", 1)
            ObjCat.Catalogo(cmbMetodoPago, "select codigo, descripcion from tblc_MetodoPago order by codigo desc", "PUE")
            ObjCat.Catalogo(cmbUsoCFD, "EXEC pCatalogos @cmd=8", 0)
            ObjCat.Catalogo(cmbMoneda, "select id, nombre from tblMoneda order by nombre", 1)
            ObjCat.Catalogo(cmbTipoRelacion, "select id, nombre from tblTipoRelacion order by nombre asc", 0)
            ObjCat.Catalogo(cmbExportacion, "select id, descripcion from tblCFDExportacion", "01")
            ObjCat.Catalogo(cmbPeriodicidad, "select id, descripcion from tblPeriodicidad", "04")
            ObjCat.Catalogo(cmbMes, "select id, descripcion from tblMeses", Format(DateTime.Now.Month, "0#"))
            ObjCat = Nothing

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblContact.Text = Resources.Resource.lblContact
            lblRFC.Text = Resources.Resource.lblRFC
            lblNumCtaPago.Text = Resources.Resource.lblNumCtaPago
            lblNumCtaPago.ToolTip = Resources.Resource.lblNumCtaPagoTooltip
            lblSubTotal.Text = Resources.Resource.lblSubTotal
            'lblImporteTasaCero.Text = Resources.Resource.lblImporteTasaCero
            lblIVA.Text = Resources.Resource.lblIVA
            lblTotal.Text = Resources.Resource.lblTotal
            txtAnio.Text = DateTime.Now.Year

            Call CargaLugarExpedicion()

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''
            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnCreateInvoice.Text = Resources.Resource.btnCreateInvoice
            btnCancelInvoice.Text = Resources.Resource.btnCancelInvoice
            '
            '   Protege contra doble clic la creación de la factura
            '
            btnCreateInvoice.Attributes.Add("onclick", "javascript:" + btnCreateInvoice.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnCreateInvoice, ""))
            '

            ''''''''''''''''''''''''''
            'Set CFD Session Variable'
            ''''''''''''''''''''''''''

            If Not String.IsNullOrEmpty(Request("id")) Then

                Session("CFD") = Request("id")

                Call CargaCFD()

                panelItemsRegistration.Visible = True
                itemsList.Visible = True
                panelResume.Visible = True

                Call DisplayItems()
                Call CargaTotales()

            Else

                Session("CFD") = 0

            End If

            'If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
            '    panelDivisas.Visible = True
            'Else
            '    panelDivisas.Visible = False
            'End If

        End If

    End Sub

    Private Sub CargaCFD()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=10, @cfdid='" & Session("CFD").ToString & "'", conn)
        Dim clienteid As Long = 0
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()


            panelSpecificClient.Visible = True
            panelItemsRegistration.Visible = True

            If rs.Read() Then
                cmbCliente.SelectedValue = rs("clienteid")
                clienteid = rs("clienteid")
                cmbUsoCFD.SelectedValue = rs("usocfdi")
                cmbDocumento.SelectedValue = rs("serieid")
                If rs("rfc") = "XAXX010101000" Then
                    panelFacturaGlobal.Visible = True
                    cmbPeriodicidad.SelectedValue = rs("periodicidad_id")
                    cmbMes.SelectedValue = rs("fac_global_mes")
                    txtAnio.Text = rs("fac_global_anio")
                End If
            End If

            rs.Close()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Call CargaCliente(clienteid)

    End Sub

    Private Sub CargaLugarExpedicion()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=3, @clienteid=1", conn)
        Dim clienteid As Long = 0
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()


            If rs.Read() Then
                'txtLugarExpedicion.Text = rs("expedicionLinea3")
            End If

            rs.Close()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

#End Region

#Region "Combobox Events"

    Private Sub CargaCliente(ByVal ClienteId As Long)
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=2, @clienteid='" & ClienteId.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            tipoprecioid = 0
            panelSpecificClient.Visible = True
            panelItemsRegistration.Visible = True

            If rs.Read() Then

                lblSocialReasonValue.Text = rs("razonsocial")
                lblContactValue.Text = rs("contacto")
                lblContactPhoneValue.Text = rs("telefono_contacto")
                lblRFCValue.Text = rs("rfc")
                'tipoprecioid = rs("tipoprecioid")
                Dim ObjData As New DataControl
                ObjData.Catalogo(cmbFormaPago, "select id, nombre from tblFormaPago order by nombre", rs("formapagoid"))
                ObjData.Catalogo(cmbCondiciones, "select id, nombre from tblCondiciones", rs("condicionesid"))
                ObjData = Nothing
                txtNumCtaPago.Text = rs("numctapago")
                tipocontribuyenteid = rs("tipocontribuyenteid")
                'Nuevos Campos
                lblStreetValue.Text = rs("fac_calle")
                lblExtNumberValue.Text = rs("fac_num_ext")
                lblIntNumberValue.Text = rs("fac_num_int")
                lblColonyValue.Text = rs("fac_colonia")
                lblCountryValue.Text = rs("fac_pais")

                lblEstadoValue.Text = rs("fac_estado")
                lblTownshipValue.Text = rs("fac_municipio")
                lblZipCodeValue.Text = rs("fac_cp")
                lblRFCValue.Text = rs("rfc")
                lblTipoContribuyenteValue.Text = rs("tipocontribuyente")

                If rs("rfc") = "XAXX010101000" Then
                    panelFacturaGlobal.Visible = True
                    cmbUsoCFD.SelectedValue = "S01"
                Else
                    panelFacturaGlobal.Visible = False
                End If
            End If
            '
            rs.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub ClearItems()

        itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
        itemsList.DataSource = Nothing
        itemsList.DataBind()

        Session("CFD") = 0
        itemsList.Visible = False

        lblSubTotalValue.Text = ""
        lblIVAValue.Text = ""
        lblTotalValue.Text = ""
        panelResume.Visible = False

    End Sub

#End Region

#Region "Add Invoice Items"

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
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Protected Sub InsertItem(ByVal id As Integer, ByVal item As GridItem)
        '
        '   Instancía objetos del grid
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim txtDescripcion As System.Web.UI.WebControls.TextBox = DirectCast(item.FindControl("txtDescripcion"), System.Web.UI.WebControls.TextBox)
        Dim txtCuenta As System.Web.UI.WebControls.TextBox = DirectCast(item.FindControl("txtCuenta"), System.Web.UI.WebControls.TextBox)
        Dim lblUnidad As Label = DirectCast(item.FindControl("lblUnidad"), Label)
        Dim txtQuantity As RadNumericTextBox = DirectCast(item.FindControl("txtQuantity"), RadNumericTextBox)
        Dim txtUnitaryPrice As RadNumericTextBox = DirectCast(item.FindControl("txtUnitaryPrice"), RadNumericTextBox)
        Dim cmbTipoPrecio As RadComboBox = DirectCast(item.FindControl("cmbTipoPrecio"), RadComboBox)
        Dim txtDescuento As RadNumericTextBox = DirectCast(item.FindControl("txtDescuento"), RadNumericTextBox)

        Dim ValidarConcepto As Boolean = False
        Dim retencionbit As Integer = 0
        Dim tiporetencion As Integer = 0

        If cmbDocumento.SelectedValue = 3 Then
            If txtCuenta.Text.Length > 0 Then
                ValidarConcepto = True
                retencionbit = 1
                tiporetencion = cmbDocumento.SelectedValue
            Else
                ValidarConcepto = False
            End If
        ElseIf cmbDocumento.SelectedValue = 6 Then
            ValidarConcepto = True
            retencionbit = 1
            tiporetencion = cmbDocumento.SelectedValue
        Else
            ValidarConcepto = True
        End If

        If ValidarConcepto = True Then


            Dim porcentaje As Double = 0
            '
            '   Agrega la partida
            '
            Dim objdata As New DataControl
            objdata.RunSQLQuery("EXEC pCFD @cmd=2, @cfdid='" & Session("CFD").ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & txtDescripcion.Text & "', @cantidad='" & txtQuantity.Text & "', @unidad='" & lblUnidad.Text & "', @precio='" & txtUnitaryPrice.Text & "', @productoid='" & id.ToString & "', @descuento='" & txtDescuento.Text & "', @retencionbit='" & retencionbit.ToString & "', @tiporetencion='" & tiporetencion.ToString & "',@cuentaPredial='" & txtCuenta.Text & "'")
            objdata = Nothing
            '
        Else
            RadWindowManager2.RadAlert("Si la Factura es Recibo de arrendamiento Favor de Agregar el Numero de la Cuenta Predial.", 330, 180, "Alert", "", "")
        End If
    End Sub

    Private Function PrecioProducto(ByVal id As String, ByVal tipoprecio As Long) As Double
        Dim precio = 0
        Dim Obj As New DataControl
        Dim txttipoPrecio As String = "unitario"
        Select Case tipoprecio
            Case 1
                txttipoPrecio = txttipoPrecio
            Case Else
                txttipoPrecio += tipoprecio.ToString
        End Select
        Try
            Dim cmd As String = "select top 1 isnull(" & txttipoPrecio & ",0) from tblMisProductos where id = '" & id & "'"
            precio = Obj.RunSQLScalarQueryDecimal(cmd)
        Catch ex As Exception
            precio = 0
        End Try

        Return precio
    End Function

    Private Sub DisplayItems()
        Dim ds As DataSet
        Dim ObjData As New DataControl
        itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
        ds = ObjData.FillDataSet("EXEC pCFD @cmd=3, @cfdid='" & Session("CFD").ToString & "'")
        itemsList.DataSource = ds
        itemsList.DataBind()
        ObjData = Nothing
        '
        'If ds.Tables(0).Rows.Count = 20 Then
        '    btnSearchItem.Enabled = False
        'Else
        '    btnSearchItem.Enabled = True
        'End If
        ''

    End Sub

    Protected Sub itemsList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles itemsList.NeedDataSource
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCFD @cmd=3, @cfdid='" & Session("CFD").ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
            itemsList.DataSource = ds
            itemsList.DataBind()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub CargaTotales()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=16, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & cmbDocumento.SelectedValue.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                tieneIva16 = rs("tieneIva16")
                tieneIvaTasaCero = rs("tieneIvaTasaCero")
                importe = rs("importe")
                iva = rs("iva")
                totalieps = rs("ieps_total")
                tipoid = rs("tipoid")
                tipoidF.Value = rs("tipoid")
                totaldescuento = rs("totaldescuento")
                total = rs("total")
                importetasacero = rs("importetasacero")
                importesindescuento = rs("importe_sindescuento")
                tipocontribuyenteid = rs("tipocontribuyenteid")

                lblSubTotalValue.Text = FormatCurrency(importe, 2).ToString
                lblDescuentoValue.Text = FormatCurrency(totaldescuento, 2).ToString
                lblIEPSValue.Text = FormatCurrency(totalieps, 2).ToString
                lblIVAValue.Text = FormatCurrency(iva, 2).ToString
                lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                lblTotalValue.Text = FormatCurrency(total, 2).ToString
                
                Select Case tipoid
                    Case 3, 6
                        If tipocontribuyenteid <> 1 Then
                            lblRetIVAValue.Text = FormatCurrency((iva / 3) * 2, 2).ToString
                            lblRetISRValue.Text = FormatCurrency((importe * 0.1), 2).ToString
                            lblTotalValue.Text = FormatCurrency((total - (importe * 0.1) - ((iva / 3) * 2)), 2).ToString
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If
                    Case 7
                        If tipocontribuyenteid <> 1 Then
                            lblRetIVAValue.Text = FormatCurrency((iva * 0.1), 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                            lblTotalValue.Text = FormatCurrency((total - (iva * 0.1)), 2).ToString
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If
                    Case Else
                        lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                        lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                End Select
                'If System.Configuration.ConfigurationManager.AppSettings("retencion4") = 1 And tipoid = 5 Then
                '    panelRetencion.Visible = True
                '    lblRetValue.Text = FormatCurrency(rs("importe") * 0.04, 2).ToString
                '    lblTotalValue.Text = FormatCurrency(rs("total") - (rs("importe") * 0.04), 2).ToString
                'End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

#End Region

#Region "Telerik Grid Items Deleting Events"

    Protected Sub itemsList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles itemsList.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Items" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ItemsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub itemsList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles itemsList.ItemCommand

        Select Case e.CommandName

            Case "cmdDelete"
                DeleteItem(e.CommandArgument)
                CargaTotales()

        End Select

    End Sub

    Private Sub DeleteItem(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd='4', @partidaId ='" & id.ToString & "'", conn)

        conn.Open()

        cmd.ExecuteReader()

        conn.Close()

        Call DisplayItems()

    End Sub

#End Region

#Region "Telerik Grid Items Column Names (From Resource File)"

    Protected Sub itemsList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles itemsList.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

            If e.Item.OwnerTableView.Name = "Items" Then

                header("codigo").Text = Resources.Resource.gridColumnNameCode
                header("descripcion").Text = Resources.Resource.gridColumnNameDescription
                header("cantidad").Text = Resources.Resource.gridColumnNameQuantity
                header("unidad").Text = Resources.Resource.gridColumnNameMeasureUnit
                'header("precio").Text = Resources.Resource.gridColumnNameUnitaryPrice
                header("importe").Text = Resources.Resource.gridColumnNameAmount

            End If

        End If

    End Sub

#End Region

#Region "Create Invoice"

    Protected Sub btnCreateInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateInvoice.Click
        If Page.IsValid Then
            Dim Timbrado As Boolean = False
            AplicaImpuestoTasa0 = False
            Dim MensageError As String = ""
            RadWindow1.VisibleOnPageLoad = False
            '
            '   Agregado para remisionar
            '
            If cmbDocumento.SelectedValue = 10 Or cmbDocumento.SelectedValue = 11 Then
                Call GeneraDocumento()
            Else
                '
                '   Rutina de generación de XML CFDI Versión 3.3
                '
                Call CargaTotales()
                '
                '   Guadar Metodo de Pago
                '
                Call GuadarMetodoPago()
                '
                m_xmlDOM = CrearDOM()
                '
                '   Verifica que tipo de comprobante se va a emitir
                '
                Dim TipoDeComprobante As String = Nothing
                Select Case tipoidF.Value
                    Case 1, 3, 4, 5, 6
                        '   Ingreso
                        TipoDeComprobante = "I"
                    Case 2, 8
                        '   Egreso (Nota de Crédito)
                        TipoDeComprobante = "E"
                End Select
                '
                '   Asigna Serie y Folio
                '
                Call AsignaSerieFolio()
                '
                Comprobante = CrearNodoComprobante(importe, total, TipoDeComprobante, tipoidF.Value)
                '
                m_xmlDOM.AppendChild(Comprobante)
                IndentarNodo(Comprobante, 1)
                '
                '  Factura al publico en general
                '
                VerificaFacturaGlobal(Comprobante)
                '
                '   Agrega CfdiRelacionados
                '
                If cmbTipoRelacion.SelectedValue.ToString <> "0" And txtFolioFiscal.Text.ToString.Length > 0 Then
                    CrearNodoCfdiRelacionados(Comprobante)
                    IndentarNodo(Comprobante, 1)
                End If
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
                '   Agrega Impuestos
                '
                CrearNodoImpuestos(Comprobante)
                IndentarNodo(Comprobante, 1)
                '
                '   Sellar Comprobante
                '
                SellarCFD(Comprobante)
                m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
                m_xmlDOM.Save(Server.MapPath("~/portalcfd/cfd_storage") & "/" & "link_" & serie.Value & folio.Value & ".xml")
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
                    'Dim TimbreSifeiVersion33 As New SIFEIPruebas33.SIFEIService()

                    'Produccion
                    Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()

                    Call Comprimir()

                    Try
                        Dim bytes() As Byte
                        bytes = TimbreSifeiVersion33.getCFDI(Usuario, Password, data, "", IdEquipo)
                        Descomprimir(bytes)
                        Timbrado = True
                    Catch ex As SoapException
                        Call cfdnotimbrado()
                        Timbrado = False
                        MensageError = ex.Message.ToString & ex.Detail.InnerText
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
                        Call cfdtimbrado()
                        '
                        cadOrigComp = CadenaOriginalComplemento(serie.Value, folio.Value)
                        '
                        '   Genera Código Bidimensional
                        '
                        Call generacbb()
                        '
                        '   Descarga Inventario si hay folio y fué timbrado el cfdi
                        '
                        If System.Configuration.ConfigurationManager.AppSettings("inventarios") = 1 Then
                            Call DescargaInventario(Session("CFD"))
                        End If
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

                '    Dim queusuariocertus As String = System.Configuration.ConfigurationManager.AppSettings("FactureHoyUsuario")
                '    Dim quepasscertus As String = System.Configuration.ConfigurationManager.AppSettings("FactureHoyContrasena")
                '    Dim queproceso As Integer = System.Configuration.ConfigurationManager.AppSettings("FactureHoyProceso")
                '    Dim MemStream As System.IO.MemoryStream = FileToMemory(Server.MapPath("cfd_storage") & "\" & "link_" & serie.Value & folio.Value & ".xml")

                '    Dim archivo As Byte() = MemStream.ToArray()
                '    Dim service As New FactureHoyNT33.WsEmisionTimbrado33Client()
                '    'Dim service As New FactureHoyPruebasV33.WsEmisionTimbrado33()
                '    Dim puerto = service.EmitirTimbrar(queusuariocertus, quepasscertus, queproceso, archivo)

                '    If Not puerto.XML Is Nothing Then
                '        If puerto.isError Then
                '            '
                '            '   Marca el cfd como no timbrado
                '            '
                '            Call cfdnotimbrado()

                '            Timbrado = False
                '            MensageError = puerto.message.Trim
                '        Else
                '            Timbrado = True
                '            MensageError = ""

                '            File.WriteAllBytes(Server.MapPath("~/portalcfd/cfd_storage") & "/" & "link_" & serie.Value & folio.Value & "_timbrado.xml", puerto.XML)
                '            cadOrigComp = puerto.cadenaOriginal
                '            '
                '            '   Genera Código Bidimensional
                '            '
                '            Call generacbb()
                '            '
                '            UUID = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "/" & "link_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                '            '
                '            '   Marca el cfd como timbrado
                '            '
                '            Call cfdtimbrado()
                '            '
                '            '   Descarga Inventario si hay folio y fué timbrado el cfdi
                '            '
                '            If System.Configuration.ConfigurationManager.AppSettings("inventarios") = 1 Then
                '                Call DescargaInventario(Session("CFD"))
                '            End If
                '            '
                '            '   Genera PDF
                '            '
                '            If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\link_" & serie.Value & folio.Value & ".pdf") Then
                '                GuardaPDF(GeneraPDF(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\link_" & serie.Value & folio.Value & ".pdf")
                '            End If
                '            '
                '        End If
                '    Else
                '        '
                '        Call cfdnotimbrado()
                '        '
                '        Timbrado = False
                '        MensageError = puerto.message.Trim
                '    End If
                'End If
            End If

            Dim cfdid As Long = 0
            cfdid = Session("CFD")

            If System.Configuration.ConfigurationManager.AppSettings("usuarios") = 1 Then
                Call AsignaCFDUsuario(cfdid)
            End If

            Session("CFD") = 0

            If Timbrado = True Then
                Response.Redirect("~/portalcfd/cfd.aspx")
            Else
                If cmbDocumento.SelectedValue = 10 Or cmbDocumento.SelectedValue = 11 Then
                    Response.Redirect("~/portalcfd/cfd.aspx")
                Else
                    txtErrores.Text = MensageError.ToString
                    RadWindow1.VisibleOnPageLoad = True
                End If
            End If
        End If
    End Sub

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

    Private Sub Comprimir()
        Dim zip As ZipFile = New ZipFile(serie.Value.ToString & folio.Value.ToString & ".zip")
        zip.AddFile(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "link_" & serie.Value & folio.Value & ".xml", "")
        Dim ms As New MemoryStream()
        zip.Save(ms)
        data = ms.ToArray
    End Sub

    Private Sub Descomprimir(ByVal data5 As Byte())
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
            System.IO.File.Copy(Path & archivo, Path & "link_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml")
        End If

        Response.Write(archivo)

    End Sub

    Private Sub DescargaInventario(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pInventario @cmd=7, @cfdid='" & cfdid.ToString & "', @userid='" & Session("userid").ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function

    Private Function CrearNodoComprobante(ByVal subTotal As Decimal, ByVal total As Decimal, ByVal TipoDeComprobante As String, ByVal tipoid As Integer) As XmlNode
        Dim Comprobante As XmlNode
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante, subTotal, total, TipoDeComprobante, tipoid)
        CrearNodoComprobante = Comprobante
    End Function

    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal subTotal As Decimal, ByVal total As Decimal, ByVal TipoDeComprobante As String, ByVal tipoid As Integer)
        Nodo.SetAttribute("xmlns:cfdi", URI_SAT)
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd")
        Nodo.SetAttribute("Version", "4.0")

        If serie.Value.ToString.Length > 0 Then
            Nodo.SetAttribute("Serie", serie.Value)
        End If

        Nodo.SetAttribute("Folio", folio.Value)
        Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss"))
        Nodo.SetAttribute("Sello", "")
        Nodo.SetAttribute("FormaPago", cmbFormaPago.SelectedValue.ToString) '01,02,03,04,05,06,07...
        Nodo.SetAttribute("NoCertificado", "")
        Nodo.SetAttribute("Certificado", "")
        Nodo.SetAttribute("CondicionesDePago", cmbCondiciones.SelectedItem.Text) 'CREDITO, CONTADO, CREDITO A 3 MESES ETC
        Nodo.SetAttribute("SubTotal", Format(subTotal, "#0.00"))

        If totaldescuento > 0 Then
            Nodo.SetAttribute("Descuento", Format(totaldescuento, "#0.00"))
        End If

        Dim moneda As String = ""
        Dim ObjData As New DataControl
        moneda = ObjData.RunSQLScalarQueryString("select isnull(clave,'') from tblMoneda where id='" & cmbMoneda.SelectedValue.ToString & "'")
        ObjData = Nothing

        If (moneda <> "MXN" And moneda <> "") Then
            Nodo.SetAttribute("Moneda", moneda)
            Nodo.SetAttribute("TipoCambio", txtTipoCambio.Text)
        Else
            Nodo.SetAttribute("Moneda", "MXN")
        End If

        Nodo.SetAttribute("Total", Format(total, "#0.00"))
        Nodo.SetAttribute("TipoDeComprobante", TipoDeComprobante)
        Nodo.SetAttribute("MetodoPago", cmbMetodoPago.SelectedValue.ToString) 'PUE, PID, PPD
        Nodo.SetAttribute("LugarExpedicion", CargaLugarExpedicionAtributos())
        Nodo.SetAttribute("Exportacion", cmbExportacion.SelectedValue.ToString) '01, 02, 03
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
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return LugarExpedicion

    End Function

    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal Regimen As String)
        Try
            Dim Emisor As XmlElement
            Emisor = CrearNodo("cfdi:Emisor")
            Emisor.SetAttribute("Nombre", nombre.ToUpper)
            Emisor.SetAttribute("Rfc", rfc)
            Emisor.SetAttribute("RegimenFiscal", Regimen)
            Nodo.AppendChild(Emisor)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
    '    Try
    '        Dim Receptor As XmlElement
    '        Receptor = CrearNodo("cfdi:Receptor")
    '        Receptor.SetAttribute("Rfc", rfc)
    '        Receptor.SetAttribute("Nombre", nombre)
    '        Receptor.SetAttribute("UsoCFDI", UsoCFDI)
    '        Nodo.AppendChild(Receptor)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement)
        Try
            Dim Certificado As String = ""
            Certificado = LeerCertificado()

            Dim Clave As String = ""
            Clave = LeerClave()

            Dim objCert As New X509Certificate2()
            Dim bRawData As Byte() = ReadFile(Server.MapPath("~/portalcfd/certificados/") & Certificado)
            objCert.Import(bRawData)
            Dim cadena As String = Convert.ToBase64String(bRawData)
            NodoComprobante.SetAttribute("NoCertificado", FormatearSerieCert(objCert.SerialNumber))
            NodoComprobante.SetAttribute("Total", Format(total, "#0.00"))
            NodoComprobante.SetAttribute("Certificado", Convert.ToBase64String(bRawData))
            NodoComprobante.SetAttribute("Sello", GenerarSello(Clave))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function ReadFile(ByVal strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = CInt(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function

    Private Function LeerCertificado() As String
        Dim Certificado As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Certificado = rs("archivo_certificado")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Certificado

    End Function

    Private Function Leerllave() As String
        Dim Llave As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Llave = rs("archivo_llave_privada")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Llave

    End Function

    Private Function LeerClave() As String
        Dim Contrasena As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Contrasena = rs("contrasena_llave_privada")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return Contrasena

    End Function

    Public Function FormatearSerieCert(ByVal Serie As String) As String
        Dim Resultado As String = ""
        Dim I As Integer
        For I = 2 To Len(Serie) Step 2
            Resultado = Resultado & Mid(Serie, I, 1)
        Next
        FormatearSerieCert = Resultado
    End Function

    Private Function CrearNodo(ByVal Nombre As String) As XmlNode
        CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, URI_SAT)
        'CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, Nothing)
    End Function

    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub

    Private Sub CrearNodoComplemento(ByVal Nodo As XmlNode)
        Dim Complemento As XmlElement

        Complemento = CrearNodo("cfdi:Complemento")
        IndentarNodo(Complemento, 1)

        Nodo.AppendChild(Complemento)
    End Sub

    Private Function FileToMemory(ByVal Filename As String) As MemoryStream
        Dim FS As New System.IO.FileStream(Filename, FileMode.Open)
        Dim MS As New System.IO.MemoryStream
        Dim BA(FS.Length - 1) As Byte
        FS.Read(BA, 0, BA.Length)
        FS.Close()
        MS.Write(BA, 0, BA.Length)
        Return MS
    End Function

    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim Cadena As String = ""
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
            Cadena = output.ToString
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return Cadena

    End Function

    Private Sub CrearNodoImpuestos(ByVal Nodo As XmlNode)
        Dim AgregarTraslado As Boolean = False
        Dim AgregarIeps As Boolean = False
        Dim TasaOCuotas As String = ""
        Dim TipoFactor As String = ""
        Dim TipoImpuesto As String = ""
        Dim Impuestos As XmlElement
        Dim Traslados As XmlElement
        Dim Traslado As XmlElement

        Call CargaTotales()

        Dim Retenciones As XmlElement
        Dim Retencion As XmlElement

        Impuestos = CrearNodo("cfdi:Impuestos")

        If iva > 0 Then
            Impuestos.SetAttribute("TotalImpuestosTrasladados", Math.Round(iva + ieps_total, 2)) 'Format(CDbl(iva + ieps_total), "0.#0")) 
        Else
            Impuestos.SetAttribute("TotalImpuestosTrasladados", "0.00")
        End If

        If ieps_total > 0 Then
            AgregarIeps = True
        End If

        If iva > 0 Then
            TasaOCuotas = "0.160000"
            AgregarTraslado = True
            TipoFactor = "Tasa"
            TipoImpuesto = "002"
        End If

        If AplicarRetencion = True Then
            '
            '   Retenciones
            '
            Select Case tipoid
                Case 3, 6   '   Recibos de honorarios o arrendamiento
                    '
                    '   Retenciones
                    '
                    If tipocontribuyenteid = 1 Then
                        'FacturaXML.total = FormatNumber(total, 4)
                    Else
                        '
                        '   ISR
                        '
                        Dim ImporteISR As Double = 0
                        Dim ImporteIVA As Double = 0

                        Retenciones = CrearNodo("cfdi:Retenciones")
                        IndentarNodo(Retenciones, 3)
                        Impuestos.AppendChild(Retenciones)

                        '
                        '   ISR
                        '
                        Retencion = CrearNodo("cfdi:Retencion")
                        Retencion.SetAttribute("Impuesto", "001")
                        ImporteISR = Math.Round((importe * 0.1), 2)

                        If ImporteISR >= 2000 Then
                            ImporteISR = Math.Round((importe * 0.1), 2)
                        End If
                        Retencion.SetAttribute("Importe", Format(CDbl(ImporteISR), "0.#0"))
                        Retenciones.AppendChild(Retencion)
                        '
                        '  IVA
                        '
                        Retencion = CrearNodo("cfdi:Retencion")
                        Retencion.SetAttribute("Impuesto", "002")
                        ImporteIVA = Math.Round((iva / 3) * 2, 2)
                        If ImporteIVA >= 2000 Then
                            ImporteIVA = Math.Round((importe * 0.106667), 2)
                        End If
                        Retencion.SetAttribute("Importe", Format(CDbl(ImporteIVA), "0.#0"))
                        Retenciones.AppendChild(Retencion)

                        IndentarNodo(Retenciones, 2)

                        Impuestos.AppendChild(Retenciones)
                        IndentarNodo(Impuestos, 1)

                        Impuestos.SetAttribute("TotalImpuestosRetenidos", Format(CDbl(ImporteISR + ImporteIVA), "0.#0"))
                        total = Math.Round((total - (ImporteISR) - (ImporteIVA)), 2)
                    End If
                Case 5
                    '
                    '   Retención del 4% Carta Porte
                    '
                    Retenciones = CrearNodo("cfdi:Retenciones")
                    IndentarNodo(Retenciones, 3)
                    Impuestos.AppendChild(Retenciones)
                    '
                    '   ISR
                    '
                    Retencion = CrearNodo("cfdi:Retencion")
                    Retencion.SetAttribute("Impuesto", "002")
                    Retencion.SetAttribute("Importe", Format(CDbl(retencion4), "0.#0"))
                    Retenciones.AppendChild(Retencion)


                    Impuestos.SetAttribute("TotalImpuestosRetenidos", Format(CDbl(retencion4), "0.#0"))
                    total = FormatCurrency(Convert.ToDecimal(FormatNumber(importe, 2)) + Convert.ToDecimal(FormatNumber(iva, 2)) - Convert.ToDecimal(FormatNumber(retencion4, 2)), 2)
                    '
                    '
                Case 7  ' Factura con Retención de 2/3 partes del IVA
                Case 11 ' Retención de 5 al millar (0.5 %)
                Case 13 ' Retención de 16%
                Case 14 ' Honorarios con Retención de 2/3 partes del IVA
            End Select
        End If

        Traslados = CrearNodo("cfdi:Traslados")
        IndentarNodo(Traslados, 3)
        Impuestos.AppendChild(Traslados)
        'TrasladosConceptos.ForEach(Function(concept) Traslados.AppendChild(concept))

        If AgregarTraslado = True Then
            Traslado = CrearNodo("cfdi:Traslado")
            Traslado.SetAttribute("Impuesto", TipoImpuesto)
            Traslado.SetAttribute("TipoFactor", TipoFactor)
            Traslado.SetAttribute("TasaOCuota", TasaOCuotas)
            If iva > 0 Then
                Traslado.SetAttribute("Importe", Format(CDbl(iva), "0.#0"))
            Else
                Traslado.SetAttribute("Importe", "0.00")
            End If
            Traslados.AppendChild(Traslado)
            Traslado.SetAttribute("Base", Format(CDbl(importe - importetasacero), "0.#0"))
        End If

        If AplicaImpuestoTasa0 = True Then
            TasaOCuotas = "0.000000"
            TipoFactor = "Tasa"
            TipoImpuesto = "002"

            Traslado = CrearNodo("cfdi:Traslado")
            Traslado.SetAttribute("Impuesto", TipoImpuesto)
            Traslado.SetAttribute("TipoFactor", TipoFactor)
            Traslado.SetAttribute("TasaOCuota", TasaOCuotas)
            Traslado.SetAttribute("Importe", "0.00")
            Traslado.SetAttribute("Base", Format(CDbl(importetasacero), "0.#0"))

            Traslados.AppendChild(Traslado)
        End If

        If AgregarIeps = True Then
            Dim ds As DataSet
            Dim ObjData1 As New DataControl
            ds = ObjData1.FillDataSet("EXEC pIepsTotal @cfdid='" & Session("CFD").ToString & "'")

            Dim tasa As String = ""
            For Each rows As DataRow In ds.Tables(0).Rows

                Traslado = CrearNodo("cfdi:Traslado")
                Traslado.SetAttribute("Impuesto", "003")
                Traslado.SetAttribute("TipoFactor", "Tasa")
                ieps = rows("ieps")
                tasa = ieps / 100

                Dim tasas As String = "0.000000"
                Dim tasaDec As Decimal = CType(tasa, Decimal)
                tasa = tasaDec.ToString(tasas)

                Traslado.SetAttribute("TasaOCuota", tasa)
                If ieps_total > 0 Then
                    Traslado.SetAttribute("Importe", Math.Round(ieps_total, 2))
                Else
                    Traslado.SetAttribute("Importe", "0.00")
                End If

                Traslados.AppendChild(Traslado)
            Next

        End If

        IndentarNodo(Traslados, 2)
        Impuestos.AppendChild(Traslados)
        IndentarNodo(Impuestos, 1)
        Nodo.AppendChild(Impuestos)

    End Sub

    Private Sub AsignaCFDUsuario(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pUsuarios @cmd=7, @userid='" & Session("userid").ToString & "', @cfdid='" & cfdid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub GeneraDocumento()
        '
        Call CargaTotales()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        '   Obtiene folio y actualiza cfd
        '
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""
        Dim tipoid As Integer = 0

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & cmbDocumento.SelectedValue.ToString & "', @instrucciones='" & instrucciones.Text & "', @formapagoid='" & cmbFormaPago.SelectedValue.ToString & "', @metodopagoid='" & cmbMetodoPago.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & cmbCondiciones.SelectedValue.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @tipocambio='" & txtTipoCambio.Text.ToString & "', @tiporelacion='" & cmbTipoRelacion.SelectedValue.ToString & "', @uuid_relacionado='" & txtFolioFiscal.Text.ToString & "'", conn)
        'Dim cmd As New SqlCommand("exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & cmbDocumento.SelectedValue.ToString & "', @instrucciones='" & instrucciones.Text & "', @fecha_factura='" & Now.ToShortDateString & "', @metodopagoid='" & cmbMetodoPago.SelectedValue.ToString & "', @formapagoid='" & cmbFormaPago.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & cmbCondiciones.SelectedValue.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            While rs.Read
                serie = rs("serie").ToString
                folio = rs("folio").ToString
                aprobacion = rs("aprobacion").ToString
                annioaprobacion = rs("annio_solicitud").ToString
                tipoid = rs("tipoid")
            End While
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        '
        '   Marca el documento como formato
        '
        Dim ObjM As New DataControl
        ObjM.RunSQLQuery("exec pCFD @cmd=33, @cfdid='" & Session("CFD").ToString & "'")
        ObjM = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        '   Genera PDF
        '
        If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\link_" & serie.ToString & folio.ToString & ".pdf") Then
            GuardaPDF(GeneraPDF_Documento(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\link_" & serie.ToString & folio.ToString & ".pdf")
        End If
        '
    End Sub

    Private Sub AsignaSerieFolio()
        '
        '   Obtiene serie y folio
        '
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & cmbDocumento.SelectedValue.ToString & "', @instrucciones='" & instrucciones.Text & "', @formapagoid='" & cmbFormaPago.SelectedValue.ToString & "', @metodopagoid='" & cmbMetodoPago.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & cmbCondiciones.SelectedValue.ToString & "', @monedaid='" & cmbMoneda.SelectedValue.ToString & "', @tipocambio='" & txtTipoCambio.Text.ToString & "', @tiporelacion='" & cmbTipoRelacion.SelectedValue.ToString & "', @uuid_relacionado='" & txtFolioFiscal.Text.ToString & "', @orden_compra='" & txtOrdenCompra.Text.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                serie.Value = rs("serie").ToString
                folio.Value = rs("folio").ToString
                aprobacion = rs("aprobacion").ToString
                annioaprobacion = rs("annio_solicitud").ToString
                tipoidF.Value = rs("tipoid")
                tipoid = rs("tipoid")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub CrearNodoCfdiRelacionados(ByVal Nodo As XmlNode)
        Dim CfdiRelacionados As XmlElement
        Dim DocumentoRelacionado As XmlElement

        CfdiRelacionados = CrearNodo("cfdi:CfdiRelacionados")
        IndentarNodo(CfdiRelacionados, 1)

        CfdiRelacionados.SetAttribute("TipoRelacion", cmbTipoRelacion.SelectedValue.ToString)
        IndentarNodo(CfdiRelacionados, 2)

        DocumentoRelacionado = CrearNodo("cfdi:CfdiRelacionado")
        DocumentoRelacionado.SetAttribute("UUID", txtFolioFiscal.Text)

        CfdiRelacionados.AppendChild(DocumentoRelacionado)
        IndentarNodo(CfdiRelacionados, 1)
        Nodo.AppendChild(CfdiRelacionados)
    End Sub

    Private Sub ConfiguraEmisor()
        '
        '   Obtiene datos del emisor
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
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
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
                'CrearNodoReceptor(Comprobante, rs("razonsocial"), rs("fac_rfc"), cmbUsoCFD.SelectedValue)
                CrearNodoReceptor(Comprobante, rs("denominacion_razon_social"), rs("fac_rfc"), cmbUsoCFD.SelectedValue, rs("fac_cp"), "", "", rs("regimenfiscalid"))
                IndentarNodo(Comprobante, 1)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connR.Close()
            connR.Dispose()
            connR = Nothing
        End Try
    End Sub

    Private Sub CrearNodoConceptos(ByVal Nodo As XmlNode)
        '
        '   Agrega Partidas
        '
        Dim Conceptos As XmlElement
        Dim Concepto As XmlElement
        Dim Impuestos As XmlElement
        Dim Traslados As XmlElement
        Dim Traslado As XmlElement
        Dim Retenciones As XmlElement
        Dim Retencion As XmlElement
        Dim CuentaPredial As XmlElement
        Dim RetencionBit As Boolean = False
        Dim conceptoid As Integer
        Dim ObjData As New DataControl

        Conceptos = CrearNodo("cfdi:Conceptos")
        IndentarNodo(Conceptos, 2)

        ObjData.RunSQLQuery("exec pCFDTraslados @cmd=6,@cfdid=" & Session("CFD"))
        ObjData.RunSQLQuery("exec pCFDRetenciones @cmd=6,@cfdid=" & Session("CFD"))

        ieps = 0
        ieps_total = 0
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD @cmd=13, @cfdId='" & Session("CFD").ToString & "'", connP)
        Try
            connP.Open()
            '
            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()
            '

            While rs.Read
                conceptoid = rs("id")
                Concepto = CrearNodo("cfdi:Concepto")
                Concepto.SetAttribute("ClaveProdServ", rs("claveprodserv"))
                Concepto.SetAttribute("NoIdentificacion", rs("codigo"))
                Concepto.SetAttribute("Cantidad", rs("cantidad"))
                Concepto.SetAttribute("ClaveUnidad", rs("claveunidad"))
                Concepto.SetAttribute("Unidad", rs("unidad"))
                Concepto.SetAttribute("Descripcion", rs("descripcion"))
                Concepto.SetAttribute("ObjetoImp", rs("objeto_impuestoid").ToString)
                ieps = rs("ieps")
                ieps_total = rs("ieps_total")

                If rs("descuento") > 0 Then
                    Concepto.SetAttribute("Descuento", Math.Round(rs("descuento"), 2))
                End If

                Concepto.SetAttribute("ValorUnitario", Format(rs("precio"), "#0.000000"))
                Concepto.SetAttribute("Importe", Format(rs("importe"), "#0.000000"))
                RetencionBit = rs("retencion")

                Impuestos = CrearNodo("cfdi:Impuestos")

                'Campos Traslados e Retenciones
                Dim Base As Decimal = 0
                Dim Impuesto As String
                Dim TipoFactor As String
                Dim TasaOCuota As String = ""
                Dim TasaCuota As String = 0
                Dim Importe As Decimal = 0

                Traslados = CrearNodo("cfdi:Traslados")
                Traslado = CrearNodo("cfdi:Traslado")

                If rs("descuento") > 0 Then
                    Base = Math.Round(rs("importe") - rs("descuento"), 2)
                    Traslado.SetAttribute("Base", Math.Round(rs("importe") - rs("descuento"), 2))
                Else
                    Traslado.SetAttribute("Base", Math.Round(rs("importe"), 2))
                    Base = Math.Round(rs("importe"), 2)
                End If

                If CDbl(rs("iva") = 0) Then
                    AplicaImpuestoTasa0 = True
                End If

                If CBool(rs("exento")) = False Then
                    Traslado.SetAttribute("Impuesto", "002")
                    Impuesto = "002"
                    Traslado.SetAttribute("TipoFactor", "Tasa")
                    TipoFactor = "Tasa"
                    Traslado.SetAttribute("TasaOCuota", rs("tasaocuota"))
                    TasaOCuota = rs("tasaocuota")
                    Traslado.SetAttribute("Importe", Format(rs("iva"), "#0.00"))
                    Importe = Format(rs("iva"), "#0.00")
                Else
                    Traslado.SetAttribute("Impuesto", "002")
                    Impuesto = "002"
                    Traslado.SetAttribute("TipoFactor", "Exento")
                    TipoFactor = "Exento"
                End If

                ObjData.RunSQLQuery("exec pCFDTraslados @cmd=1, " &
                                     "@cfdid=" & Session("CFD") &
                                     ",@partidaid=" & conceptoid &
                                     ",@baseTraslado='" & Base &
                                     "',@impuesto ='" & Impuesto &
                                     "',@tipofactor='" & TipoFactor &
                                     "',@tasaOcuota='" & TasaOCuota &
                                     "',@importe=" & Importe)

                Traslados.AppendChild(Traslado)


                If ieps >= 1 Then
                    Traslado = CrearNodo("cfdi:Traslado")

                    If rs("descuento") > 0 Then
                        Base = Math.Round(rs("importe") - rs("descuento"), 2)
                        Traslado.SetAttribute("Base", Format(Base, "#0.00"))
                    Else
                        Base = Math.Round(rs("importe"), 2)
                        Traslado.SetAttribute("Base", Format(Base, "#0.00"))
                    End If

                    Traslado.SetAttribute("Impuesto", "003")
                    Impuesto = "003"
                    Traslado.SetAttribute("TipoFactor", "Tasa")
                    TipoFactor = "Tasa"
                    TasaCuota = ieps / 100

                    Dim tasa As String = "0.000000"
                    Dim tasaDec As Decimal = CType(TasaCuota, Decimal)
                    TasaCuota = tasaDec.ToString(tasa)

                    Traslado.SetAttribute("TasaOCuota", TasaCuota)
                    Importe = Math.Round(rs("ieps_total"), 2)
                    Traslado.SetAttribute("Importe", Format(Importe, "#0.00"))

                    Traslados.AppendChild(Traslado)

                    ObjData.RunSQLQuery("exec pCFDTraslados @cmd=1, " &
                                     "@cfdid=" & Session("CFD") &
                                     ",@partidaid=" & conceptoid &
                                     ",@baseTraslado='" & Base &
                                     "',@impuesto ='" & Impuesto &
                                     "',@tipofactor='" & TipoFactor &
                                     "',@tasaOcuota='" & TasaOCuota &
                                     "',@importe=" & Importe)
                End If

                Impuestos.AppendChild(Traslados)

                If RetencionBit = True Then
                    If rs("tipoRetencion") > 0 Then
                        If rs("tipoRetencion") = 3 Or rs("tipoRetencion") = 6 Then 'Recibos de honorarios o arrendamiento
                            Retenciones = CrearNodo("cfdi:Retenciones")
                            Retencion = CrearNodo("cfdi:Retencion")

                            '
                            '   IVA
                            '
                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Base = Format(CDbl(rs("importe")), "#.#0")
                            Retencion.SetAttribute("Impuesto", "001")
                            Impuesto = "001"
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            TipoFactor = "Tasa"
                            Importe = Math.Round(CDec(rs("importe") * 0.1), 2)

                            If Importe >= 2000 Then
                                Importe = Math.Round(CDec(rs("importe") * 0.1), 2)
                            End If

                            Retencion.SetAttribute("TasaOCuota", "0.100000")
                            TasaOCuota = "0.100000"
                            Retencion.SetAttribute("Importe", Format(CDbl(Importe), "#.#0"))

                            Retenciones.AppendChild(Retencion)

                            Retencion = CrearNodo("cfdi:Retencion")
                            '
                            '   Retenciones
                            '
                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Base = Format(CDbl(rs("importe")), "#.#0")
                            Retencion.SetAttribute("Impuesto", "002")
                            Impuesto = "002"
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            TipoFactor = "Tasa"
                            Importe = Math.Round(CDec(rs("importe")), 2)

                            If Importe >= 2000 Then
                                Importe = Format(CDbl(Importe * 0.106667), "#.#0")
                                Retencion.SetAttribute("TasaOCuota", "0.106667")
                                TasaOCuota = "0.106667"
                            Else
                                Importe = Math.Round((rs("iva") / 3) * 2, 2)
                                Retencion.SetAttribute("TasaOCuota", "0.106666")
                                TasaOCuota = "0.106666"
                            End If

                            Retencion.SetAttribute("Importe", Math.Round(Importe, 2))

                            Retenciones.AppendChild(Retencion)

                            Impuestos.AppendChild(Retenciones)

                            AplicarRetencion = True

                            ObjData.RunSQLQuery("exec pCFDRetenciones @cmd=1, " &
                                     "@cfdid=" & Session("CFD") &
                                     ",@partidaid=" & conceptoid &
                                     ",@baseRetencion='" & Base &
                                     "',@impuesto ='" & Impuesto &
                                     "',@tipofactor='" & TipoFactor &
                                     "',@tasaOcuota='" & TasaOCuota &
                                     "',@importe=" & Importe)

                        ElseIf rs("tipoRetencion") = 5 Then ' Retención del 4% Carta Porte
                            '
                            Retenciones = CrearNodo("cfdi:Retenciones")
                            Retencion = CrearNodo("cfdi:Retencion")

                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Base = Format(CDbl(rs("importe")), "#.#0")
                            Retencion.SetAttribute("Impuesto", "002")
                            Impuesto = "002"
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            TipoFactor = "Tasa"
                            Retencion.SetAttribute("TasaOCuota", "0.040000")
                            Importe = Format(CDbl(rs("importe") * 0.04), "#.#0")
                            If Importe >= 2000 Then
                                Importe = Math.Round(rs("importe") * 0.04) + 0.01
                                Retencion.SetAttribute("TasaOCuota", "0.040000")
                                TasaOCuota = "0.040000"
                            Else
                                Retencion.SetAttribute("TasaOCuota", "0.040000")
                                TasaOCuota = "0.040000"
                            End If
                            Retencion.SetAttribute("Importe", Format(CDbl(rs("importe") * 0.04), "#.#0"))

                            Retenciones.AppendChild(Retencion)

                            Impuestos.AppendChild(Retenciones)

                            AplicarRetencion = True

                            ObjData.RunSQLQuery("exec pCFDRetenciones @cmd=1, " &
                                     "@cfdid=" & Session("CFD") &
                                     ",@partidaid=" & conceptoid &
                                     ",@baseTraslado='" & Base &
                                     "',@impuesto ='" & Impuesto &
                                     "',@tipofactor='" & TipoFactor &
                                     "',@tasaOcuota='" & TasaOCuota &
                                     "',@importe=" & Importe)

                        ElseIf rs("tipoRetencion") = 7 Then  ' Factura con Retención de 2/3 partes del IVA
                            '
                            '   IVA
                            '
                            Retenciones = CrearNodo("cfdi:Retenciones")
                            Retencion = CrearNodo("cfdi:Retencion")

                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Retencion.SetAttribute("Impuesto", "002")
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            Retencion.SetAttribute("TasaOCuota", " 0.040000")
                            Retencion.SetAttribute("Importe", Format(CDbl(rs("importe") * 0.04), "#.#0"))

                            Retenciones.AppendChild(Retencion)

                            Impuestos.AppendChild(Retenciones)
                            '
                            '   Retenciones
                            '
                            '
                            'FacturaXML.Impuestos.Retenciones = New ComprobanteImpuestosRetencion() {Retencion}
                            'FacturaXML.Impuestos.totalImpuestosRetenidosSpecified = True
                            'FacturaXML.Impuestos.totalImpuestosRetenidos = FormatNumber(((iva * 2) / 3), 4)
                            'FacturaXML.total = FormatNumber((total - ((iva * 2) / 3)), 4)
                            '
                            '
                            ObjData.RunSQLQuery("exec pCFDRetenciones @cmd=1, " &
                                     "@cfdid=" & Session("CFD") &
                                     ",@partidaid=" & conceptoid &
                                     ",@baseTraslado='" & Base &
                                     "',@impuesto ='" & Impuesto &
                                     "',@tipofactor='" & TipoFactor &
                                     "',@tasaOcuota='" & TasaOCuota &
                                     "',@importe=" & Importe)

                            AplicarRetencion = True
                        End If

                    End If
                End If

                Concepto.AppendChild(Impuestos)

                If cmbDocumento.SelectedValue = 3 Then
                    CuentaPredial = CrearNodo("cfdi:CuentaPredial")
                    CuentaPredial.SetAttribute("Numero", rs("cuentaPredial"))

                    Concepto.AppendChild(CuentaPredial)
                End If

                'If chkAduana.Checked = True Then
                '    Aduanera = CrearNodo("cfdi:InformacionAduanera")
                '    Aduanera.SetAttribute("NumeroPedimento", numeropedimento.Text)
                '    Concepto.AppendChild(Aduanera)
                'End If

                Conceptos.AppendChild(Concepto)
                IndentarNodo(Conceptos, 2)
                Concepto = Nothing
            End While
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try

        Nodo.AppendChild(Conceptos)

    End Sub

    Private Sub cfdnotimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub cfdtimbrado()
        Dim Objdata As New DataControl
        If Not String.IsNullOrEmpty(Request("cid")) Then
            Objdata.RunSQLQuery("exec pCFD @cmd=24, @uuid='" & UUID.ToString & "', @cfdid='" & Session("CFD").ToString & "', @cotizacionid='" & Request("cid").ToString & "', @cotizacion_timbradabit=1")
        ElseIf Not String.IsNullOrEmpty(Request("rid")) Then
            Objdata.RunSQLQuery("exec pCFD @cmd=24, @uuid='" & UUID.ToString & "', @cfdid='" & Session("CFD").ToString & "', @remisionid='" & Request("rid").ToString & "', @remision_timbradabit=1")
        Else
            Objdata.RunSQLQuery("exec pCFD @cmd=24, @uuid='" & UUID.ToString & "', @cfdid='" & Session("CFD").ToString & "'")
        End If
        Objdata = Nothing
    End Sub

    Private Sub GuadarMetodoPago()
        Dim Objdata As New DataControl
        Dim cmdFacturaGlobal As String = ""
        If panelFacturaGlobal.Visible Then
            cmdFacturaGlobal = "',@periodicidad_id='" & cmbPeriodicidad.SelectedValue &
                               "',@fac_global_mes='" & cmbMes.SelectedValue &
                               "',@fac_global_anio='" & txtAnio.Text
        End If
        Objdata.RunSQLQuery("exec pCFD @cmd=25, @metodopagoid='" & cmbMetodoPago.SelectedValue & "', @usocfdi='" & cmbUsoCFD.SelectedValue & "', @serieid='" & cmbDocumento.SelectedValue & cmdFacturaGlobal & "', @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub obtienellave()
        Dim connX As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdX As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                archivoLlavePrivada = Server.MapPath("~/portalcfd/llave") & "\" & rs("archivo_llave_privada")
                contrasenaLlavePrivada = rs("contrasena_llave_privada")
                archivoCertificado = Server.MapPath("~/portalcfd/certificados") & "\" & rs("archivo_certificado")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connX.Close()
            connX.Dispose()
            connX = Nothing
        End Try
    End Sub

    Private Function Parametros(ByVal codigoUsuarioProveedor As String, ByVal codigoUsuario As String, ByVal idSucursal As Integer, ByVal textoXml As String) As String
        Dim root As XmlNode
        Dim xmlParametros As New XmlDocument()

        If xmlParametros.ChildNodes.Count = 0 Then
            Dim declarationNode As XmlNode = xmlParametros.CreateXmlDeclaration("1.0", "UTF-8", String.Empty)

            xmlParametros.AppendChild(declarationNode)

            root = xmlParametros.CreateElement("Parametros")
            xmlParametros.AppendChild(root)
        Else
            root = xmlParametros.DocumentElement
            root.RemoveAll()
        End If

        Dim attribute As XmlAttribute = root.OwnerDocument.CreateAttribute("Version")
        attribute.Value = "1.0"
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("CodigoUsuarioProveedor")
        attribute.Value = codigoUsuarioProveedor
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("CodigoUsuario")
        attribute.Value = codigoUsuario
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("IdSucursal")
        attribute.Value = idSucursal.ToString()
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("TextoXml")
        attribute.Value = textoXml
        root.Attributes.Append(attribute)

        Return xmlParametros.InnerXml
    End Function

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
        rfcE = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.Value & folio.Value & "_timbrado.xml", "Rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.Value & folio.Value & "_timbrado.xml", "Rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.Value & folio.Value & "_timbrado.xml", "Total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.Value & folio.Value & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
        sello = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.Value & folio.Value & "_timbrado.xml", "SelloCFD", "tfd:TimbreFiscalDigital")
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

        'Dim cadena As String = ""
        'UUID = ""
        'Dim rfcE As String = ""
        'Dim rfcR As String = ""
        'Dim total As String = ""

        ''
        ''   Obtiene datos del cfdi para construir string del CBB
        ''

        ''
        'rfcE = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.Value & folio.Value & "_timbrado.xml", "Rfc", "cfdi:Emisor")
        'rfcR = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.Value & folio.Value & "_timbrado.xml", "Rfc", "cfdi:Receptor")
        'total = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.Value & folio.Value & "_timbrado.xml", "Total", "cfdi:Comprobante")
        'UUID = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.Value & folio.Value & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
        ''
        'Dim fmt As String = "0000000000.000000"
        'Dim totalDec As Decimal = CType(total, Decimal)
        'total = totalDec.ToString(fmt)
        ''
        'cadena = "?re=" & rfcE.ToString & "&rr=" & rfcR.ToString & "&tt=" & total.ToString & "&id=" & UUID.ToString
        ''
        'Response.Write(cadena)
        ''   Genera gráfico
        ''
        'Dim qrCodeEncoder As New QRCodeEncoder
        'qrCodeEncoder.QRCodeEncodeMode = qrCodeEncoder.ENCODE_MODE.BYTE
        'qrCodeEncoder.QRCodeScale = 4
        'qrCodeEncoder.QRCodeVersion = 8
        'qrCodeEncoder.QRCodeErrorCorrect = qrCodeEncoder.ERROR_CORRECTION.Q
        'Dim image As Drawing.Image

        'image = qrCodeEncoder.Encode(cadena)
        'image.Save(Server.MapPath("~/portalCFD/cbb") & "\" & serie.Value & folio.Value & ".png", System.Drawing.Imaging.ImageFormat.Png)
        ' ''
    End Sub

    Private Function TotalPartidas(ByVal cfdId As Long) As Long
        Dim Total As Long = 0
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD @cmd=15, @cfdid='" & cfdId.ToString & "'", connP)
        Try

            connP.Open()

            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()

            If rs.Read Then
                Total = rs("total")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try
        Return Total
    End Function

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
        FlujoReader = New XmlTextReader(Server.MapPath("cfd_storage") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
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

        Dim numeroaprobacion As String = ""
        Dim anoAprobacion As String = ""
        Dim fechaHora As String = ""
        Dim noCertificado As String = ""
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
        Dim condiciones As String = ""
        Dim enviara As String = ""
        Dim instrucciones As String = ""
        Dim pedimento As String = ""
        Dim retencion As Decimal = 0
        Dim tipoid As Integer = 0
        Dim divisaid As Integer = 1
        Dim expedicionLinea1 As String = ""
        Dim expedicionLinea2 As String = ""
        Dim expedicionLinea3 As String = ""
        Dim porcentaje As Decimal = 0
        Dim plantillaid As Integer = 1
        Dim tipopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""
        Dim serie As String = ""
        Dim folio As Integer = 0
        Dim usoCFDI As String = ""
        Dim tipo_comprobante As String = ""
        Dim tiporelacion As String = ""
        Dim uuid_relacionado As String = ""
        Dim logoFormato As String = ""
        Dim orden_compra As String = ""

        Dim ds As DataSet = New DataSet

        Try
            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
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
                divisaid = rs("divisaid")
                fechaHora = rs("fecha_factura").ToString
                condiciones = "Condiciones: " & rs("condiciones").ToString
                enviara = rs("enviara").ToString
                instrucciones = rs("instrucciones")
                If rs("aduana") = "" Or rs("numero_pedimento") = "" Then
                    pedimento = ""
                Else
                    pedimento = "Aduana: " & rs("aduana") & vbCrLf & "Fecha: " & rs("fecha_pedimento").ToString & vbCrLf & "Número: " & rs("numero_pedimento").ToString
                End If
                expedicionLinea1 = rs("expedicionLinea1")
                expedicionLinea2 = rs("expedicionLinea2")
                expedicionLinea3 = rs("expedicionLinea3")
                porcentaje = rs("porcentaje")
                plantillaid = rs("plantillaid")
                logoFormato = rs("logoformato")
                tipocontribuyenteid = rs("tipocontribuyenteid")
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
                usoCFDI = rs("usocfdi")
                tiporelacion = rs("tiporelacion")
                uuid_relacionado = rs("uuid_relacionado")
                orden_compra = rs("orden_compra")
            End If
            rs.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)

        If divisaid = 1 Then
            CantidadTexto = "Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
        Else
            CantidadTexto = "Son " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD"
        End If
        If panelFacturaGlobal.Visible Then
            instrucciones = instrucciones + " | " + "Factura Global con Periodicidad " & cmbPeriodicidad.SelectedItem.Text & ", Correspondiente al Mes de " & cmbMes.SelectedItem.Text & " del Año " & txtAnio.Text & "."
        End If
        Select Case tipoid
            Case 3, 6, 7      ' honorarios y arrendamiento
                Dim reporte As New Formatos.formato_cfdi_honorarios33
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 3
                        reporte.ReportParameters("txtDocumento").Value = "Arrendamiento No.    " & serie.ToString & folio.ToString
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                    Case 7
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")

                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & logoFormato)

                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtPACCertifico").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara

                If tipocontribuyenteid = 1 Then
                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                    'reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                    reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                    reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                    reporte.ReportParameters("txtRetIva").Value = FormatCurrency(0, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
                    '
                    '   Ajusta cantidad con texto
                    '
                    total = FormatNumber((importe + iva), 2)
                    largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                    CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    '
                Else
                    If tipoid = 7 Then
                        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                        'reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                        reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva * 0.1), 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - ((iva * 0.1)), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva) - ((iva * 0.1)), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                    Else
                        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                        'reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(importe * 0.1, 2).ToString
                        reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva / 3) * 2, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        If divisaid = 1 Then
                            CantidadTexto = "Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
                        Else
                            CantidadTexto = "Son " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD"
                        End If
                    End If
                End If
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                Return reporte
            Case Else
                Dim reporte As New formato_cfdi_osdan
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 1, 4, 7
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                    Case 2, 8
                        reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
                    Case 5
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO     EFECTOS FISCALES AL PAGO"
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")

                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & logoFormato)

                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtPACCertifico").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe, 2).ToString
                reporte.ReportParameters("txtIEPS").Value = GetImpuestoTrasladoXML(serie.ToString, folio.ToString, "003")
                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                'reporte.ReportParameters("txtIVA").Value = GetImpuestoTrasladoXML(serie.ToString, folio.ToString, "002")
                reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                reporte.ReportParameters("txtDescuento").Value = FormatCurrency(totaldescuento, 2).ToString

                reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
                reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString

                reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                tipo_comprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")

                If tipo_comprobante = "I" Then
                    tipo_comprobante = "Ingreso"
                ElseIf tipo_comprobante = "E" Then
                    tipo_comprobante = "Egreso"
                ElseIf tipo_comprobante = "N" Then
                    tipo_comprobante = "Nómina"
                ElseIf tipo_comprobante = "P" Then
                    tipo_comprobante = "Pago"
                ElseIf tipo_comprobante = "T" Then
                    tipo_comprobante = "Traslado"
                End If

                reporte.ReportParameters("txtTipoComprobante").Value = tipo_comprobante

                If tipoid = 2 Then
                    reporte.ReportParameters("txtTipoRelacion").Value = "Tipo Relación: " & tiporelacion
                    reporte.ReportParameters("txtUUIDRelacionado").Value = "UUID Relacionado: " & uuid_relacionado
                ElseIf tipoid = 5 Then
                    retencion = FormatNumber((importe * 0.04), 2)
                    reporte.ReportParameters("txtRetencion").Value = FormatCurrency(retencion, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
                    largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                    If divisaid = 1 Then
                        CantidadTexto = "Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N."
                    Else
                        CantidadTexto = "Son " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD"
                    End If
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                End If

                If tiporelacion.Length > 0 And uuid_relacionado.Length > 0 Then
                    reporte.ReportParameters("txtTipoRelacion").Value = "Tipo Relación: " & tiporelacion
                    reporte.ReportParameters("txtUUIDRelacionado").Value = "UUID Relacionado: " & uuid_relacionado
                End If

                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = "Uso de CFDI: " & usoCFDI
                reporte.ReportParameters("txtOrdenCompra").Value = orden_compra

                Return reporte

        End Select

    End Function

    Private Function GetImpuestoTrasladoXML(ByVal serie As String, ByVal folio As String, ByVal tipoImpuesto As String) As String
        Dim path As String
        path = Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml"
        Dim result As String
        result = GetXmlAttributeMatch(path, "Importe", "cfdi:Traslado", "Impuesto", tipoImpuesto)
        Return FormatCurrency(result, 2).ToString
    End Function

    Public Function GetXmlAttributeMatch(ByVal url As String, ByVal campo As String, ByVal nodo As String, ByVal atributoMacth As String, ByVal valorMatch As String) As String
        Dim valor As String = ""
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        Dim nodoimpuestos As String = "cfdi:Impuestos"
        Dim atributoIndicador As String = "TotalImpuestosTrasladados"
        FlujoReader = New XmlTextReader(url)
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        Try
            While FlujoReader.Read()
                Select Case FlujoReader.NodeType
                    Case XmlNodeType.Element
                        '
                        '
                        If FlujoReader.Name.ToString.ToUpper = nodo.ToUpper Then
                            '
                            '
                            If FlujoReader.AttributeCount >= 1 Then
                                For k = 0 To FlujoReader.AttributeCount - 1
                                    FlujoReader.MoveToAttribute(k)
                                    '
                                    '
                                    For i = 0 To FlujoReader.AttributeCount - 1
                                        FlujoReader.MoveToAttribute(i)
                                        '
                                        '
                                        If FlujoReader.Name = atributoMacth And FlujoReader.Value.ToString = valorMatch Then
                                            For j = 0 To FlujoReader.AttributeCount - 1

                                                FlujoReader.MoveToAttribute(j)
                                                If FlujoReader.Name = campo Then
                                                    valor = FlujoReader.Value.ToString
                                                End If
                                            Next
                                        End If
                                        '
                                        '
                                    Next
                                    '
                                    '
                                Next
                            End If
                        End If
                        '
                        '
                End Select
            End While
        Catch ex As Exception
        End Try

        If valor = "" Then
            valor = "0"
        End If

        Return valor

    End Function

    Private Function GeneraPDF_Documento(ByVal cfdid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim numeroaprobacion As String = ""
        Dim anoAprobacion As String = ""
        Dim fechaHora As String = ""
        Dim noCertificado As String = ""
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
        Dim rec_razonsocial As String = ""
        Dim rec_callenum As String = ""
        Dim rec_colonia As String = ""
        Dim rec_ciudad As String = ""
        Dim rec_rfc As String = ""

        Dim folio_aprobacion As String = ""
        Dim folio_emision As String = ""
        Dim folio_vigencia As String = ""
        Dim folio_rango As String = ""

        Dim importe As Decimal = 0
        Dim importetasacero As Decimal = 0
        Dim iva As Decimal = 0
        Dim total As Decimal = 0
        Dim CantidadTexto As String = ""
        Dim condiciones As String = ""
        Dim enviara As String = ""
        Dim instrucciones As String = ""
        Dim pedimento As String = ""
        Dim retencion As Decimal = 0
        Dim tipoid As Integer = 0
        Dim divisaid As Integer = 1
        Dim expedicionLinea1 As String = ""
        Dim expedicionLinea2 As String = ""
        Dim expedicionLinea3 As String = ""
        Dim porcentaje As Decimal = 0
        Dim plantillaid As Integer = 1
        Dim codigo_cbb As String = ""
        Dim tipopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""
        Dim serie As String = ""
        Dim folio As Integer = 0

        Dim ds As DataSet = New DataSet

        Try
            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                serie = rs("serie")
                folio = rs("folio")
                tipoid = rs("tipoid")
                tipoid = cmbDocumento.SelectedValue
                em_razonsocial = rs("em_razonsocial")
                em_callenum = rs("em_callenum")
                em_colonia = rs("em_colonia")
                em_ciudad = rs("em_ciudad")
                em_rfc = rs("em_rfc")
                em_regimen = rs("regimen")
                '
                rec_razonsocial = rs("razonsocial")
                rec_callenum = rs("callenum")
                rec_colonia = rs("colonia")
                rec_ciudad = rs("ciudad")
                rec_rfc = rs("rfc")
                '
                folio_aprobacion = rs("folio_aprobacion")
                folio_emision = rs("folio_emision")
                folio_vigencia = rs("folio_vigencia")
                folio_rango = rs("folio_rango")
                '
                razonsocial = rs("razonsocial")
                callenum = rs("callenum")
                colonia = rs("colonia")
                ciudad = rs("ciudad")
                rfc = rs("rfc")
                importe = rs("importe")
                importetasacero = rs("importetasacero")
                iva = rs("iva")
                total = rs("total")
                divisaid = rs("divisaid")
                fechaHora = rs("fecha_factura").ToString
                condiciones = "Condiciones: " & rs("condiciones").ToString
                enviara = rs("enviara").ToString
                instrucciones = rs("instrucciones")
                If rs("aduana") = "" Or rs("numero_pedimento") = "" Then
                    pedimento = ""
                Else
                    pedimento = "Aduana: " & rs("aduana") & vbCrLf & "Fecha: " & rs("fecha_pedimento").ToString & vbCrLf & "Número: " & rs("numero_pedimento").ToString
                End If
                expedicionLinea1 = rs("expedicionLinea1")
                expedicionLinea2 = rs("expedicionLinea2")
                expedicionLinea3 = rs("expedicionLinea3")
                porcentaje = rs("porcentaje")
                plantillaid = rs("plantillaid")
                tipocontribuyenteid = rs("tipocontribuyenteid")
                codigo_cbb = rs("codigo_cbb")
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
            End If
            rs.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)

        If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
            If divisaid = 1 Then
                CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
            Else
                CantidadTexto = "( Son " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
            End If
        Else
            CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
        End If

        Dim reporte As New Formatos.formato_cbb
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("txtFechaEmision").Value = Now.ToShortDateString & TimeOfDay.ToString("h:mm:ss tt")

        Select Case tipoid
            Case 7  '   Pedido
                reporte.ReportParameters("txtDocumento").Value = "Pedido No.    " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: SEÑALAR FECHA DE ENTREGA_________________________________"
                reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
            Case 8
                reporte.ReportParameters("txtDocumento").Value = "Pago No. " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: GUARDAR COMPROBANTE PARA CUALQUIER ACLARACIÓN"
                reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
            Case 9
                reporte.ReportParameters("txtDocumento").Value = "Orden de Compra No. " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: FAVOR DE ANEXAR ORDEN DE COMPRA A FACTURA"
                reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
            Case 10
                reporte.ReportParameters("txtDocumento").Value = "Cotización No. " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
            Case 11
                reporte.ReportParameters("txtDocumento").Value = "Remisión No. " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
            Case 12
                reporte.ReportParameters("txtDocumento").Value = "Nota de Salida No.    " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: VERIFICAR CANT. DE PRODUCTOS QUE SALEN DEL ALMACÉN"
                reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
            Case 13
                reporte.ReportParameters("txtDocumento").Value = "Póliza No.    " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ANEXAR PÓLIZA A FACTURA"
                reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
            Case 16
                reporte.ReportParameters("txtDocumento").Value = "Req. de Mat. No. " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "Cotizar__ Solicitar al Proveedor __ Tiempo de Entrega______ Opción de Pago_______"
                reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
            Case 17
                reporte.ReportParameters("txtDocumento").Value = "Nómina No.    " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones1").Value = "RECIBÍ DE CONFORMIDAD EL SALARIO DEVENGADO POR LOS DAIS QUE LABORE EN EL PERIODO DE TIEMPO QUE DETERMINA ESTE"
                reporte.ReportParameters("txtObservaciones2").Value = "RECIBO, NO RESERVANDOME ACCIÓN QUE EJERCITAR NI PRESENTE  NI FUTURA EN CONTRA DE LA EMPRESA POR CONCEPTO"
                reporte.ReportParameters("txtObservaciones3").Value = "DE 4 SUELDOS, DIFERENCIAS DE SALARIO, HORAS EXTRAS, SÉPTIMOS DÍAS, DÍAS FESTIVOS NI DE NINGUNA NATURALEZA"
                reporte.ReportParameters("txtObservaciones4").Value = "PERCEPCIONES Y DEDUCCIONES DE TRABAJADOR"
        End Select

        reporte.ReportParameters("txtNoAprobacion").Value = "Aprobación No. " & folio_aprobacion.ToString
        reporte.ReportParameters("txtEmision").Value = folio_emision.ToString
        reporte.ReportParameters("txtRango").Value = folio_rango.ToString
        reporte.ReportParameters("txtCondicionesPago").Value = condiciones
        reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/nocbb.png")
        reporte.ReportParameters("txtLeyenda").Value = ""
        reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
        reporte.ReportParameters("txtClienteRazonSocial").Value = rec_razonsocial.ToString
        reporte.ReportParameters("txtClienteCalleNum").Value = rec_callenum.ToString
        reporte.ReportParameters("txtClienteColonia").Value = rec_colonia.ToString
        reporte.ReportParameters("txtClienteCiudadEstado").Value = rec_ciudad.ToString
        reporte.ReportParameters("txtClienteRFC").Value = rec_rfc.ToString
        reporte.ReportParameters("txtPedimento").Value = pedimento
        reporte.ReportParameters("txtEnviarA").Value = "Comentarios: " & instrucciones
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3

        If porcentaje > 0 Then
            reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
        End If

        reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
        reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
        reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString

        If numctapago.Length > 0 Then
            reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
        End If
        
        Return reporte

    End Function

#End Region

#Region "Telerik Autocomplete"


#End Region

    Protected Sub serieid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDocumento.SelectedIndexChanged
        If cmbDocumento.SelectedValue > 0 Then
            cmbDocumento.Enabled = False
            If cmbDocumento.SelectedValue = 2 Then
                lblTipoRelacion.Visible = True
                cmbTipoRelacion.Visible = True
                valTipoRelecion.Enabled = True
                lblUUID.Visible = True
                txtFolioFiscal.Visible = True
                valFolioFiscal.Enabled = True
            End If
        End If
    End Sub

    Protected Sub btnCancelSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelSearch.Click
        gridResults.Visible = False
        itemsList.Visible = True
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = False
    End Sub

    Protected Sub gridResults_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gridResults.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                If Session("CFD") = 0 Then
                    GetCFD()
                End If

                InsertItem(e.CommandArgument, e.Item)
                DisplayItems()
                GetAplicarRetencion()
                Call CargaTotales()
                panelResume.Visible = True
                gridResults.Visible = False
                itemsList.Visible = True
                txtSearchItem.Text = ""
                txtSearchItem.Focus()
                btnCancelSearch.Visible = False
        End Select
    End Sub

    Private Sub GetAplicarRetencion()
        Dim ObjData As New DataControl
        AplicarRetencion = ObjData.RunSQLScalarQueryString("SELECT top 1 isnull(retencion,0) FROM tblCFD_Partidas WHERE cfdid='" & Session("CFD").ToString & "' order by id desc")
        ObjData = Nothing
    End Sub

    Protected Sub txtSearchItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchItem.TextChanged
        gridResults.Visible = True
        itemsList.Visible = False
        Dim objdata As New DataControl
        gridResults.DataSource = objdata.FillDataSet("exec pCFD @cmd=30, @txtSearch='" & txtSearchItem.Text & "', @clienteid='" & cmbCliente.SelectedValue.ToString & "'")
        gridResults.DataBind()

        If cmbDocumento.SelectedValue <> 3 Then
            gridResults.MasterTableView.GetColumn("predial").Visible = False
        End If

        objdata = Nothing
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = True
    End Sub

    Protected Sub gridResults_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridResults.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem

                Dim itm As Telerik.Web.UI.GridDataItem
                itm = CType(e.Item, Telerik.Web.UI.GridDataItem)

                'Dim defaultprecio As Integer = itm.GetDataKeyValue("precio")

                Dim unitario1 As String = itm.GetDataKeyValue("unitario")
                Dim unitario2 As String = itm.GetDataKeyValue("unitario2")
                Dim unitario3 As String = itm.GetDataKeyValue("unitario3")
                Dim unitario4 As String = itm.GetDataKeyValue("unitario4")


                Dim txtQuantity As RadNumericTextBox = DirectCast(e.Item.FindControl("txtQuantity"), RadNumericTextBox)
                Dim txtUnitaryPrice As RadNumericTextBox = DirectCast(e.Item.FindControl("txtUnitaryPrice"), RadNumericTextBox)
                'Dim txtCuenta As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtCuenta"), System.Web.UI.WebControls.TextBox)
                'Dim cmbTipoPrecio As DropDownList = DirectCast(e.Item.FindControl("cmbTipoPrecio"), DropDownList)

                txtQuantity.Text = "1"
                txtUnitaryPrice.Text = e.Item.DataItem("precio")
                'cmbTipoPrecio.SelectedValue = defaultprecio
                'txtCuenta.Visible = False

        End Select
    End Sub

    Protected Sub cmbClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCliente.SelectedIndexChanged
        Call CargaCliente(cmbCliente.SelectedValue)
        Call ClearItems()
    End Sub

    Protected Sub btnCancelInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelInvoice.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pCFD @cmd=31, @cfdid='" & Session("CFD").ToString & "'")
        ObjData = Nothing
        '
        Session("CFD") = 0
        '
        Response.Redirect("~/portalcfd/cfd.aspx")
        ''
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Response.Redirect("~/portalcfd/cfd.aspx")
    End Sub

    Private Sub cmbMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMoneda.SelectedIndexChanged
        If cmbMoneda.SelectedValue <> 1 Then
            txtTipoCambio.Enabled = True
            valTipoCambio.Enabled = True
        Else
            txtTipoCambio.Text = 0
            txtTipoCambio.Enabled = False
            valTipoCambio.Enabled = False
        End If
    End Sub
#Region "Factura Global"
    Private Sub VerificaFacturaGlobal(ByVal Nodo As XmlNode)
        If panelFacturaGlobal.Visible Then
            Dim InformacionGlobal As XmlElement = CrearNodo("cfdi:InformacionGlobal")
            InformacionGlobal.SetAttribute("Periodicidad", cmbPeriodicidad.SelectedValue)
            InformacionGlobal.SetAttribute("Meses", cmbMes.SelectedValue)
            InformacionGlobal.SetAttribute("Año", txtAnio.Text)
            Nodo.AppendChild(InformacionGlobal)
        End If
    End Sub
#End Region
End Class