Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Xml.Schema
Imports System.Collections
Imports Org.BouncyCastle.Crypto
Imports System.Xml.Xsl
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports uCFDsLib
Imports uCFDsLib.v3
Imports FirmaSAT.Sat
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Util

Partial Class portalcfd_Factura
    Inherits System.Web.UI.Page
    Private importe As Decimal = 0
    Private iva As Decimal = 0
    Private total As Decimal = 0
    Private totaldescuento As Decimal = 0
    Private tieneIvaTasaCero As Boolean = False
    Private tieneIva16 As Boolean = False
    Private archivoLlavePrivada As String = ""
    Private contrasenaLlavePrivada As String = ""
    Private archivoCertificado As String = ""
    Private _selloCFD As String = ""
    Private _cadenaOriginal As String = ""
    Private serie As String = ""
    Private folio As Long = 0
    Private FacturaXML As New uCFDsLib.v3.Comprobante
    Private tipocontribuyenteid As Integer = 0
    Private tipoid As Integer = 0
    Private tipoprecioid As Integer

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

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

            Dim TelerikRadComboBox As New FillRadComboBox
            TelerikRadComboBox.FillRadComboBox(cmbClient, "EXEC pMisClientes @cmd=1, @clienteUnionId='" & Session("clienteid") & "' ")

            cmbClient.Text = Resources.Resource.cmbEmptyMessage

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblContact.Text = Resources.Resource.lblContact
            lblContactPhone.Text = Resources.Resource.lblContactPhone
            lblRFC.Text = Resources.Resource.lblRFC

            lblCode.Text = Resources.Resource.lblCode
            lblDescription.Text = Resources.Resource.lblDescription
            lblQuantity.Text = Resources.Resource.lblQuantity
            lblUnitaryPrice.Text = Resources.Resource.lblUnitaryPrice
            lblUnit.Text = Resources.Resource.lblUnit

            lblSubTotal.Text = Resources.Resource.lblSubTotal
            lblIVA.Text = "IVA = "
            lblTotal.Text = Resources.Resource.lblTotal

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''
            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator3.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator4.Text = Resources.Resource.validatorMessage

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddItem.Text = Resources.Resource.btnAddItem
            btnCreateInvoice.Text = Resources.Resource.btnCreateInvoice

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

            Dim ObjCat As New DataControl
            ObjCat.Catalogo(serieid, "select distinct isnull(a.serie,'Sin serie'), isnull(b.nombre,'Sin serie') from tblMisFolios a inner join tblTipoDocumento b on a.tipoid=b.id order by isnull(b.nombre,'Sin serie')", 0)
            ObjCat.Catalogo(tasaid, "select id, nombre from tblTasa", 0)
            ObjCat = Nothing


            If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
                panelDivisas.Visible = True
            Else
                panelDivisas.Visible = False
            End If

        End If

    End Sub

    Private Sub CargaCFD()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD  @cmd=10, @cfdid='" & Session("CFD").ToString & "'", conn)
        Dim clienteid As Long = 0
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            txtQuantity.Value = "0"
            txtUnitaryPrice.Value = "0"

            panelSpecificClient.Visible = True
            panelItemsRegistration.Visible = True

            If rs.Read() Then

                cmbClient.SelectedValue = rs("clienteid")
                clienteid = rs("clienteid")

            End If

            rs.Close()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try
        '
        Call CargaCliente(clienteid)
        '
    End Sub

#End Region

#Region "Combobox Events"

    Private Sub CargaCliente(ByVal ClienteId As Long)
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pMisClientes  @cmd=2, @clienteid='" & ClienteId.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            cmbCode.Text = ""
            lblSearchResult.Text = ""
            txtDescription.Text = ""
            txtQuantity.Value = "0"
            txtUnitaryPrice.Value = "0"
            txtUnit.Text = ""
            tipoprecioid = 0
            panelSpecificClient.Visible = True
            panelItemsRegistration.Visible = True

            If rs.Read() Then

                lblSocialReasonValue.Text = rs("razonsocial")
                lblContactValue.Text = rs("contacto")
                lblContactPhoneValue.Text = rs("telefono_contacto")
                lblRFCValue.Text = rs("rfc")
                lblCondicionesValue.Text = rs("condiciones")
                lblTipoPrecioValue.Text = rs("tipoprecio")
                tipoprecioid = rs("tipoprecioid")
            End If
            '
            tipocontribuyenteid = rs("tipocontribuyenteid")

            rs.Close()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


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

    Protected Sub cmbClient_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbClient.SelectedIndexChanged

        Call CargaCliente(cmbClient.SelectedValue)
        Call ClearItems()

        Dim TelerikRadComboBox As New FillRadComboBox
        TelerikRadComboBox.FillRadComboBox(cmbCode, "EXEC pCFD @cmd=20, @codigo='" & cmbCode.Text & "', @clienteprincipalid='" & Session("clienteid") & "'")

    End Sub

#End Region

#Region "Add Invoice Items"

    Protected Sub btnAddItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddItem.Click

        If Session("CFD") = 0 Then

            GetCFD()
            InsertItem()
            DisplayItems()
            Call CargaTotales()

        Else

            InsertItem()
            DisplayItems()
            Call CargaTotales()

        End If

        Dim TelerikRadComboBox As New FillRadComboBox
        TelerikRadComboBox.FillRadComboBox(cmbCode, "EXEC pCFD @cmd=20, @codigo='" & cmbCode.Text & "', @clienteprincipalid='" & Session("clienteid").ToString & "'")

        cmbCode.Text = ""
        txtDescription.Text = ""
        txtQuantity.Value = "0"
        txtUnitaryPrice.Value = "0"
        txtUnit.Text = ""

        itemsList.Visible = True

        panelResume.Visible = True

    End Sub

    Protected Sub GetCFD()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=1, @clienteid='" & cmbClient.SelectedValue & "', @tasaid='" & tasaid.SelectedValue.ToString & "'", conn)

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
            '

        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Protected Sub InsertItem()

        '
        Dim descuentobit As Integer = 0
        If chkDescuento.Checked = True Then
            descuentobit = 1
        Else
            descuentobit = 0
        End If
        '


        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=2, @cfdid='" & Session("CFD").ToString & "', @codigo='" & cmbCode.Text & "', @descripcion='" & txtDescription.Text & "', @cantidad='" & txtQuantity.Text & "', @unidad='" & txtUnit.Text & "', @precio='" & txtUnitaryPrice.Text & "', @productoid='" & productoid.Value.ToString & "', @descuentobit='" & descuentobit.ToString & "', @tasaid='" & tasaid.SelectedValue.ToString & "'", conn)

        Try

            conn.Open()

            cmd.ExecuteReader()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub DisplayItems()
        Dim ds As DataSet
        Dim ObjData As New DataControl
        itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
        ds = ObjData.FillDataSet("EXEC pCFD @cmd=3, @cfdid='" & Session("CFD").ToString & "'")
        itemsList.DataSource = ds
        itemsList.DataBind()
        ObjData = Nothing
        '
        If ds.Tables(0).Rows.Count = 20 Then
            btnAddItem.Enabled = False
        Else
            btnAddItem.Enabled = True
        End If
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
            '
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub CargaTotales()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=16, @cfdid='" & Session("CFD").ToString & "', @tipocambio='" & tipocambio.Text & "', @serie='" & serieid.SelectedValue.ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                tieneIva16 = rs("tieneIva16")
                tieneIvaTasaCero = rs("tieneIvaTasaCero")
                importe = rs("importe")
                iva = rs("iva")
                tipoid = rs("tipoid")
                totaldescuento = rs("totaldescuento")
                total = rs("total")


                '
                lblSubTotalValue.Text = FormatCurrency(rs("importe_pesos"), 2).ToString
                lblIVAValue.Text = FormatCurrency(rs("iva_pesos"), 2).ToString
                lblTotalValue.Text = FormatCurrency(rs("total_pesos"), 2).ToString
                '
                '
                If System.Configuration.ConfigurationManager.AppSettings("retencion4") = 1 And tipoid = 5 Then
                    panelRetencion.Visible = True
                    lblRetValue.Text = FormatCurrency(rs("importe") * 0.04, 2).ToString
                    lblTotalValue.Text = FormatCurrency(rs("total") - (rs("importe") * 0.04), 2).ToString
                End If
                '

            End If

        Catch ex As Exception
            '
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
                header("precio").Text = Resources.Resource.gridColumnNameUnitaryPrice
                header("importe").Text = Resources.Resource.gridColumnNameAmount

            End If

        End If

    End Sub

#End Region

#Region "Create Invoice"

    Protected Sub btnCreateInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateInvoice.Click
        '
        Call CargaTotales()




        Dim FacturaEmisor As New uCFDsLib.v3.ComprobanteEmisor
        Dim FacturaDomicilioFiscal As New uCFDsLib.v3.t_UbicacionFiscal
        Dim FacturaReceptor As New uCFDsLib.v3.ComprobanteReceptor
        Dim FacturaDomicilioReceptor As New uCFDsLib.v3.t_Ubicacion
        Dim FacturaImpuestos As New uCFDsLib.v3.ComprobanteImpuestos


        FacturaXML.fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))
        FacturaXML.subTotal = importe
        FacturaXML.total = total
        If totaldescuento > 0 Then
            FacturaXML.descuento = totaldescuento
            FacturaXML.descuentoSpecified = True
        End If


        FacturaXML.noCertificado = ""
        FacturaXML.certificado = ""
        FacturaXML.sello = ""

        FacturaXML.formaDePago = "Pago en una sola exhibición"

        If (tipoid = 4 Or tipoid = 8) Then
            FacturaXML.Moneda = "USD"
            FacturaXML.TipoCambio = tipocambio.Text
        Else
            FacturaXML.Moneda = "MXN"
        End If


        '
        '   Obtiene datos del emisor
        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=11, @clienteId='" & Session("clienteid").ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                With FacturaDomicilioFiscal
                    .calle = rs("fac_calle")
                    .noExterior = rs("fac_num_ext")
                    If rs("fac_num_int").ToString.Length > 0 Then
                        .noInterior = rs("fac_num_int")
                    End If
                    .colonia = rs("fac_colonia")
                    .municipio = rs("fac_municipio")
                    .estado = rs("fac_estado")
                    .pais = "México"
                    .codigoPostal = rs("fac_cp")
                End With

                With FacturaEmisor
                    .nombre = rs("razonsocial")
                    .rfc = rs("fac_rfc")
                    .DomicilioFiscal = FacturaDomicilioFiscal
                End With

            End If

        Catch ex As Exception
            txtDescription.Text = ex.ToString
        Finally
            conn.Close()
        End Try
        '
        FacturaXML.Emisor = FacturaEmisor
        '
        '
        '   Obtiene datos del receptor
        '
        Dim connR As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdR As New SqlCommand("exec pCFD @cmd=12, @clienteId='" & cmbClient.SelectedValue.ToString & "'", connR)
        Try

            connR.Open()

            Dim rs As SqlDataReader
            rs = cmdR.ExecuteReader()

            If rs.Read Then

                With FacturaDomicilioReceptor
                    .calle = rs("fac_calle")
                    .noExterior = rs("fac_num_ext")
                    If rs("fac_num_int").ToString.Length > 0 Then
                        .noInterior = rs("fac_num_int")
                    End If
                    .colonia = rs("fac_colonia")
                    .municipio = rs("fac_municipio")
                    .estado = rs("fac_estado")
                    .pais = "México"
                    .codigoPostal = rs("fac_cp")
                End With

                With FacturaReceptor
                    .nombre = rs("razonsocial")
                    .rfc = rs("fac_rfc")
                    .Domicilio = FacturaDomicilioReceptor
                End With
                tipocontribuyenteid = rs("tipocontribuyenteid")
            End If

        Catch ex As Exception
            txtDescription.Text = ex.ToString
        Finally
            connR.Close()
            connR.Dispose()
            connR = Nothing
        End Try
        '
        '
        FacturaXML.Receptor = FacturaReceptor
        '
        '
        '   Agrega Partidas
        '
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD @cmd=13, @cfdId='" & Session("CFD").ToString & "', @serie='" & serieid.SelectedValue.ToString & "', @tipocambio='" & tipocambio.Text & "'", connP)
        Try

            connP.Open()

            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()

            Dim conceptos(TotalPartidas(Session("CFD"))) As uCFDsLib.v3.ComprobanteConcepto
            Dim contador As Long = 0
            While rs.Read

                Dim FacturaPartida As New uCFDsLib.v3.ComprobanteConcepto
                FacturaPartida.cantidad = rs("cantidad")
                FacturaPartida.descripcion = rs("descripcion")
                FacturaPartida.unidad = rs("unidad")
                FacturaPartida.valorUnitario = rs("precio")
                FacturaPartida.importe = rs("importe")


                If chkAduana.Checked = True Then


                    '
                    '   Información aduanera
                    '
                    '
                    Dim itemAduana(1) As uCFDsLib.v3.t_InformacionAduanera
                    Dim itemAduanaData As New uCFDsLib.v3.t_InformacionAduanera

                    With itemAduanaData
                        .aduana = nombreaduana.Text
                        .fecha = fechapedimento.SelectedDate.Value
                        .numero = numeropedimento.Text
                    End With

                    itemAduana(0) = itemAduanaData

                    FacturaPartida.Items = itemAduana
                End If

                conceptos(contador) = FacturaPartida






                contador += 1
            End While

            FacturaXML.Conceptos = conceptos



        Catch ex As Exception
            txtDescription.Text = ex.ToString
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try
        '
        '
        '
        '
        '   Obtiene serie y folio
        '

        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""

        Dim SQLUpdate As String = ""

        If Not chkAduana.Checked Then
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @serie='" & serieid.SelectedValue.ToString & "', @enviara='" & enviara.Text & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='', @numero_pedimento='" & numeropedimento.Text & "', @tipocambio='" & tipocambio.Text & "'"
        Else
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @serie='" & serieid.SelectedValue.ToString & "', @enviara='" & enviara.Text & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='" & fechapedimento.SelectedDate.Value.ToShortDateString & "', @numero_pedimento='" & numeropedimento.Text & "', @tipocambio='" & tipocambio.Text & "'"
        End If

        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand(SQLUpdate, connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie = rs("serie").ToString
                folio = rs("folio").ToString
                aprobacion = rs("aprobacion").ToString
                annioaprobacion = rs("annio_solicitud").ToString
                tipoid = rs("tipoid")
            End If
        Catch ex As Exception
            txtDescription.Text = ex.ToString
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        FacturaXML.folio = folio
        If serie.Length > 0 Then
            FacturaXML.serie = serie
        End If
        '
        '
        '
        '
        '   Agrega impuestos
        '
        '
        Select Case tipoid
            Case 3, 6
                '
                '   IVA
                '
                Dim traslados(1) As uCFDsLib.v3.ComprobanteImpuestosTraslado
                Dim Impuestos As New uCFDsLib.v3.ComprobanteImpuestos
                Impuestos.totalImpuestosTrasladados = iva
                Impuestos.totalImpuestosTrasladadosSpecified = True

                Dim TrasladoIva As New uCFDsLib.v3.ComprobanteImpuestosTraslado
                TrasladoIva.importe = FormatNumber(iva, 4)
                TrasladoIva.impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA
                Select Case tasaid.SelectedValue
                    Case 1
                        TrasladoIva.tasa = 0
                    Case 2
                        TrasladoIva.tasa = 11
                    Case 3
                        TrasladoIva.tasa = 16
                    Case Else
                        TrasladoIva.tasa = 16
                End Select

                traslados(0) = TrasladoIva
                Impuestos.Traslados = traslados
                '
                FacturaXML.Impuestos = Impuestos
                '
                '   Retenciones
                '
                If tipocontribuyenteid = 1 Then
                    FacturaXML.total = FormatNumber(total, 4)
                Else
                    '
                    '   ISR
                    '
                    Dim retenciones(2) As uCFDsLib.v3.ComprobanteImpuestosRetencion
                    Dim RetencionISR As New uCFDsLib.v3.ComprobanteImpuestosRetencion
                    RetencionISR.importe = FormatNumber((importe * 0.1), 4)
                    RetencionISR.impuesto = ComprobanteImpuestosRetencionImpuesto.ISR
                    retenciones(0) = RetencionISR
                    '
                    '   IVA
                    '
                    Dim RetencionIVA As New uCFDsLib.v3.ComprobanteImpuestosRetencion
                    RetencionIVA.importe = FormatNumber((iva / 3) * 2, 4)
                    RetencionIVA.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
                    retenciones(1) = RetencionIVA
                    '
                    FacturaXML.Impuestos.Retenciones = retenciones
                    FacturaXML.total = FormatNumber((total - (importe * 0.1) - ((iva / 3) * 2)), 4)
                End If
            Case 7  ' Retención del 10%
                '
                '   IVA
                '
                Dim traslados(1) As uCFDsLib.v3.ComprobanteImpuestosTraslado
                Dim Impuestos As New uCFDsLib.v3.ComprobanteImpuestos
                Impuestos.totalImpuestosTrasladados = iva
                Impuestos.totalImpuestosTrasladadosSpecified = True

                Dim TrasladoIva As New uCFDsLib.v3.ComprobanteImpuestosTraslado
                TrasladoIva.importe = FormatNumber(iva, 4)
                TrasladoIva.impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA
                Select Case tasaid.SelectedValue
                    Case 1
                        TrasladoIva.tasa = 0
                    Case 2
                        TrasladoIva.tasa = 11
                    Case 3
                        TrasladoIva.tasa = 16
                    Case Else
                        TrasladoIva.tasa = 16
                End Select
                traslados(0) = TrasladoIva
                Impuestos.Traslados = traslados
                '
                FacturaXML.Impuestos = Impuestos
                '
                '   Retenciones
                '
                If tipocontribuyenteid = 1 Then
                    FacturaXML.total = FormatNumber(total, 4)
                Else
                    '
                    '
                    '   IVA
                    '
                    Dim retenciones(1) As uCFDsLib.v3.ComprobanteImpuestosRetencion
                    Dim RetencionIVA As New uCFDsLib.v3.ComprobanteImpuestosRetencion
                    RetencionIVA.importe = FormatNumber((iva / 3) * 2, 4)
                    RetencionIVA.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
                    retenciones(0) = RetencionIVA
                    '
                    FacturaXML.Impuestos.Retenciones = retenciones
                    FacturaXML.total = FormatNumber((total - ((iva / 3) * 2)), 4)
                    '
                End If
            Case Else
                Dim traslados(1) As uCFDsLib.v3.ComprobanteImpuestosTraslado
                Dim Impuestos As New uCFDsLib.v3.ComprobanteImpuestos
                Impuestos.totalImpuestosTrasladados = iva
                Impuestos.totalImpuestosTrasladadosSpecified = True

                Dim TrasladoIva As New uCFDsLib.v3.ComprobanteImpuestosTraslado
                TrasladoIva.importe = iva
                TrasladoIva.impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA
                '
                Select Case tasaid.SelectedValue
                    Case 1
                        TrasladoIva.tasa = 0
                    Case 2
                        TrasladoIva.tasa = 11
                    Case 3
                        TrasladoIva.tasa = 16
                    Case Else
                        TrasladoIva.tasa = 16
                End Select
                '
                traslados(0) = TrasladoIva
                Impuestos.Traslados = traslados
                '
                FacturaXML.Impuestos = Impuestos
                '
        End Select
        '
        '
        '
        '   Retención de 4%
        '
        If System.Configuration.ConfigurationManager.AppSettings("retencion4") = 1 And tipoid = 5 Then
            '
            Dim retenciones(1) As uCFDsLib.v3.ComprobanteImpuestosRetencion
            Dim Retencion As New uCFDsLib.v3.ComprobanteImpuestosRetencion
            Retencion.importe = importe * 0.04
            Retencion.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
            retenciones(0) = Retencion
            '
            FacturaXML.Impuestos.Retenciones = retenciones
            FacturaXML.total = total - (importe * 0.04)
            '
            '
        End If
        '
        '
        '
        '   Verifica que tipo de comprobante se va a emitir
        '
        Select Case tipoid
            Case 1, 3, 4, 5, 6
                FacturaXML.tipoDeComprobante = ComprobanteTipoDeComprobante.ingreso
            Case 2, 8
                FacturaXML.tipoDeComprobante = ComprobanteTipoDeComprobante.egreso
        End Select
        '
        '   Obtiene llave y contraseña
        '
        Call obtienellave()
        '
        '   Lee certificado
        '
        Call leecertificado()
        '
        '   Genera sello
        '
        Call generarSello()
        '
        '   Guarda XML
        '
        generarXmlDoc().Save(Server.MapPath("cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & ".xml")
        '
        '   Realiza Timbrado
        '
        If TimbradoFacturaxion() = True Then
            '
            '   Guarda XML ya timbrado   
            '
            generarXmlDoc().Save(Server.MapPath("cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & "_timbrado.xml")
            '
            '   Genera Código Bidimensional
            '
            Call generacbb()
            '
            '
            '   Marca el cfd como timbrado
            '
            Call cfdtimbrado()
            '
            '
            '   Genera PDF
            '
            If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & ".pdf") Then
                Response.Write(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & ".pdf")
                GuardaPDF(GeneraPDF(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & ".pdf")

            End If
            '
            '
            '   Genera PDF Pre-Impreso
            '
            If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & "_preimpreso.pdf") Then
                GuardaPDF(GeneraPDF_PreImpreso(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & "_preimpreso.pdf")
            End If
            '
            '
            '
        Else
            '
            '   Marca el cfd como no timbrado
            '
            Call cfdnotimbrado()
            '
        End If
        '

        Dim cfdid As Long = 0
        cfdid = Session("CFD")
        Session("CFD") = 0
        '
        Response.Redirect("~/portalcfd/cfd.aspx")
        ''
    End Sub

    Private Sub cfdnotimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub cfdtimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=24, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub leecertificado()
        Dim ObjCert As New Certificado(archivoCertificado)
        FacturaXML.certificado = ObjCert.CertificadoBase64
        FacturaXML.noCertificado = ObjCert.Serie
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
            '
        Finally

            connX.Close()
            connX.Dispose()
            connX = Nothing

        End Try
    End Sub

    Private Function generarXmlDoc() As XmlDocument
        Try
            Dim stream As New System.IO.MemoryStream()
            Dim xmlNameSpace As New XmlSerializerNamespaces()
            xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance")
            xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3")
            Dim xmlTextWriter As New XmlTextWriter(stream, Encoding.UTF8)
            xmlTextWriter.Formatting = Formatting.Indented
            Dim xs As New XmlSerializer(GetType(Comprobante))
            xs.Serialize(xmlTextWriter, FacturaXML, xmlNameSpace)

            Dim doc As New System.Xml.XmlDocument()
            stream.Position = 0
            'stream.Seek(0, SeekOrigin.Begin)
            doc.Load(stream)

            Dim schemaLocation As XmlAttribute = doc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance")
            schemaLocation.Value = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv3.xsd"
            doc.DocumentElement.SetAttributeNode(schemaLocation)

            Dim schemaLocation2 As XmlAttribute = doc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance")
            schemaLocation2.Value = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv3.xsd"
            doc.DocumentElement.SetAttributeNode(schemaLocation2)

            xmlTextWriter.Close()

            Return doc
        Catch ex As Exception
            Throw New Exception("Error al generar XmlDocument, Error: " & ex.Message)
        End Try
    End Function

    Private Sub generarSello()
        Try
            Dim esquema As String = Server.MapPath("~/portalcfd/SAT/cadenaoriginal_3_0.xslt")
            If File.Exists(esquema) Then
                Dim xmlDoc As XmlDocument = generarXmlDoc()
                Dim transformador As New XslCompiledTransform()
                transformador.Load(esquema)
                Dim CadenaOriginal As New StringWriter()
                transformador.Transform(xmlDoc.CreateNavigator(), Nothing, CadenaOriginal)

                _cadenaOriginal = CadenaOriginal.ToString()


                Dim rutaKey As String = archivoLlavePrivada
                Dim claveKey As String = contrasenaLlavePrivada
                'Generar SELLO
                Try
                    If File.Exists(rutaKey) Then
                        Dim pass As String = claveKey
                        Dim dataKey() As Byte = File.ReadAllBytes(rutaKey)
                        Dim asp As Org.BouncyCastle.Crypto.AsymmetricKeyParameter = Org.BouncyCastle.Security.PrivateKeyFactory.DecryptKey(pass.ToCharArray(), dataKey)
                        Dim ms As New MemoryStream()
                        Dim writer As TextWriter = New StreamWriter(ms)
                        Dim stWrite As New System.IO.StringWriter()
                        Dim pmw As Org.BouncyCastle.OpenSsl.PemWriter = New PemWriter(stWrite)
                        pmw.WriteObject(asp)
                        stWrite.Close()

                        'ISigner sig = SignerUtilities.GetSigner("MD5WithRSAEncryption");
                        Dim sig As ISigner = SignerUtilities.GetSigner("SHA1WithRSA")

                        ' Convierte a UTF8
                        Dim plaintext() As Byte = Encoding.UTF8.GetBytes(CadenaOriginal.ToString())

                        ' Sella
                        sig.Init(True, asp)
                        sig.BlockUpdate(plaintext, 0, plaintext.Length)
                        Dim signature() As Byte = sig.GenerateSignature()

                        Dim signatureHeader As Object = Convert.ToBase64String(signature)

                        'Asignar sello
                        FacturaXML.sello = signatureHeader.ToString()
                        _selloCFD = signatureHeader.ToString()

                        '
                        '   Guarda sello y cadena en la BD
                        '
                        Dim ObjData As New DataControl
                        ObjData.RunSQLQuery("update tblCFD set sello='" & _selloCFD.ToString & "', cadenaoriginal='" & _cadenaOriginal.ToString & "' where id='" & Session("CFD").ToString & "'")
                        ObjData = Nothing
                        '
                    Else
                        Throw New Exception("Error al generar Sello, el archivo llave no existe: " & rutaKey)
                    End If
                Catch ex As Exception
                    Throw New Exception("Error al generar Sello, Error: " & ex.Message)
                End Try
            Else
                Throw New Exception("El archivo esquema: " & esquema & " No se encunetra en la ruta.")
            End If
        Catch ex As Exception
            Throw New Exception("Error al generar cadena original, Error: " & ex.Message)
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

    Private Function TimbradoFacturaxion() As Boolean
        '
        '
        Dim timbradoExitoso As Boolean = False
        '
        '
        Try
            '
            '   Convierte a texto el XML
            '
            Dim sw As New StringWriter
            Dim xw As New XmlTextWriter(sw)
            generarXmlDoc.WriteTo(xw)
            '
            '   Invoca al webservice de Facturaxion
            '
            Dim ServicioFX As New WSFX.TimbreFiscalDigitalSoapClient
            Dim params As String = ""
            Dim codigoUsuarioProveedor As String
            Dim codigoUsuario As String
            Dim idSucursal As Integer

            If System.Configuration.ConfigurationManager.AppSettings("fx_pruebas") = 1 Then
                codigoUsuarioProveedor = System.Configuration.ConfigurationManager.AppSettings("fx_codigousuarioproveedor")
                codigoUsuario = System.Configuration.ConfigurationManager.AppSettings("fx_codigousuario")
                idSucursal = System.Configuration.ConfigurationManager.AppSettings("fx_idSucursal")
            Else
                codigoUsuarioProveedor = System.Configuration.ConfigurationManager.AppSettings("fx_codigousuarioproveedor_prod")
                codigoUsuario = System.Configuration.ConfigurationManager.AppSettings("fx_codigousuario_prod")
                idSucursal = System.Configuration.ConfigurationManager.AppSettings("fx_idSucursal_prod")
            End If

            params = Parametros(codigoUsuarioProveedor, codigoUsuario, idSucursal, sw.ToString)
            '
            '   Timbra y obtiene resultado
            '
            Dim resultado As String = ""
            Dim resultadoAcuse As String = ""
            Dim DocXMLTimbrado As New XmlDocument
            Dim DocXMLTimbradoAcuseSAT As New XmlDocument

            If System.Configuration.ConfigurationManager.AppSettings("fx_pruebas") = 1 Then
                timbradoExitoso = ServicioFX.GenerarTimbrePrueba(params, resultado)
            Else
                timbradoExitoso = ServicioFX.GenerarTimbre(params, resultado)
                'Dim FILENAME As String = Server.MapPath("~/portalcfd/cfd_storage" & "\Output.txt")

                ''Get a StreamWriter class that can be used to write to the file
                'Dim objStreamWriter As StreamWriter
                'objStreamWriter = File.AppendText(FILENAME)

                ''Append the the end of the string, "A user viewed this demo at: "
                ''followed by the current date and time
                'objStreamWriter.WriteLine("se usó timbrado normal." & DateTime.Now.ToString())

                ''Close the stream
                'objStreamWriter.Close()

            End If


            Dim DocParams As New XmlDocument
            DocParams.LoadXml(params)
            DocParams.Save(Server.MapPath("cfd_storage") & "\" & "iu_params_" & serie.ToString & folio.ToString & ".xml")

            DocXMLTimbrado.LoadXml(resultado)
            '
            '   Guarda XML del Timbre
            '
            DocXMLTimbrado.Save(Server.MapPath("cfd_storage") & "\" & "iu_timbre_" & serie.ToString & folio.ToString & ".xml")
            '
        Catch exT As Exception
            Response.Write(exT.ToString)
            Response.End()
        End Try
        '
        '
        If timbradoExitoso Then
            '
            '   Obtiene los valores del timbre de respuesta
            '
            Dim selloSAT As String = ""
            Dim noCertificadoSAT As String = ""
            Dim selloCFD As String = ""
            Dim fechaTimbrado As String = ""
            Dim UUID As String = ""
            Dim Version As String = ""
            '
            '
            Dim s_RutaRespuestaPAC As String = Server.MapPath("cfd_storage") & "\" & "iu_timbre_" & serie.ToString & folio.ToString & ".xml"
            Dim respuestaPAC As New Timbrado()
            Dim objStreamReader As New StreamReader(s_RutaRespuestaPAC)
            Dim Xml As New XmlSerializer(respuestaPAC.[GetType]())
            respuestaPAC = DirectCast(Xml.Deserialize(objStreamReader), Timbrado)
            objStreamReader.Close()

            '
            'Crear el objeto timbre para asignar los valores de la respuesta PAC
            Dim timbre As New TimbreFiscalDigital()
            timbre.FechaTimbrado = Convert.ToDateTime(respuestaPAC.Items(0).Informacion(0).Timbre(0).FechaTimbrado)
            timbre.noCertificadoSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).noCertificadoSAT
            timbre.selloCFD = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloCFD
            timbre.selloSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloSAT
            timbre.UUID = respuestaPAC.Items(0).Informacion(0).Timbre(0).UUID
            timbre.version = respuestaPAC.Items(0).Informacion(0).Timbre(0).version
            '
            '
            'Convertir el objeto TimbreFiscal a un nodo
            Dim stream As New System.IO.MemoryStream()
            Dim xmlNameSpace As New XmlSerializerNamespaces()
            xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")

            Dim xmlTextWriter As New XmlTextWriter(stream, Encoding.UTF8)
            xmlTextWriter.Formatting = Formatting.None
            Dim xs As New XmlSerializer(GetType(TimbreFiscalDigital))
            xs.Serialize(xmlTextWriter, timbre, xmlNameSpace)
            Dim doc As New System.Xml.XmlDocument()
            stream.Position = 0
            doc.Load(stream)

            Dim schemaLocation As XmlAttribute = doc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance")
            schemaLocation.Value = "http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/TimbreFiscalDigital/TimbreFiscalDigital.xsd"
            doc.DocumentElement.SetAttributeNode(schemaLocation)

            xmlTextWriter.Close()
            doc.PreserveWhitespace = True
            '
            '
            '   De un objeto Comprobante ya existente, asignar el timbre fiscal
            '
            Dim elemento As XmlElement() = {doc.DocumentElement}
            Dim complemento As New ComprobanteComplemento()
            complemento.Any = elemento
            FacturaXML.Complemento = complemento
            '
            '
        Else
            '
            '   Obtiene y guarda el mensaje de error
            '
            '
            '   
            '
            '
        End If
        '
        Return timbradoExitoso
        ''
    End Function

    Private Sub Timbrado()
        '
        '   Obtiene datos del emisor
        '
        Dim timbre_usuario As String = ""
        Dim timbre_password As String = ""
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=11, @clienteId='" & Session("clienteid").ToString & "'", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                timbre_usuario = rs("timbre_usuario")
                timbre_password = rs("timbre_password")
            End If

        Catch ex As Exception
            txtDescription.Text = ex.ToString
        Finally
            conn.Close()
        End Try
        '
        '   Convierte a texto el XML
        '
        Dim sw As New StringWriter
        Dim xw As New XmlTextWriter(sw)
        generarXmlDoc.WriteTo(xw)

        '
        '
        '   Tibra con Folios Digitales
        '
        Dim ServicioFD As New WSFD.WS_FD_TFDSoapClient
        Dim Respuesta As New WSFD.ArrayOfString
        '
        Respuesta = ServicioFD.TimbrarPruebaCFDI(timbre_usuario, timbre_password, sw.ToString)
        '
        '   Guarda XML Timbrado o en su defecto el error
        '
        If (Respuesta(0).Length = 0 And Respuesta(1).Length = 0 And Respuesta(2).Length = 0) Then
            Dim DocXMLTimbrado As New XmlDocument
            Dim DocXMLTimbradoAcuseSAT As New XmlDocument
            DocXMLTimbrado.LoadXml(Respuesta(3))
            DocXMLTimbradoAcuseSAT.LoadXml(Respuesta(4))
            '
            '   Guarda XML ya timbrado
            '
            DocXMLTimbrado.Save(Server.MapPath("cfd_storage") & "\" & "iu_timbrado_" & serie.ToString & folio.ToString & ".xml")
            '
            '   Guarda acuse de recibo del SAT
            '
            DocXMLTimbradoAcuseSAT.Save(Server.MapPath("cfd_storage") & "\" & "iu_timbrado_acuseSAT_" & serie.ToString & folio.ToString & ".xml")
            '
        Else
            Dim ObjData As New DataControl
            ObjData.RunSQLQuery("update tblCFD set error1='" & Respuesta(0).ToString & "', error2='" & Respuesta(1).ToString & "', error3='" & Respuesta(2).ToString & "' where id='" & Session("CFD").ToString & "'")
            ObjData = Nothing
        End If
    End Sub

    Private Sub generacbb()
        Dim cadena As String = ""
        Dim UUID As String = ""
        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""

        '
        '   Obtiene datos del cfdi para construir string del CBB
        '

        '
        rfcE = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
        '
        Dim fmt As String = "0000000000.000000"
        Dim totalDec As Decimal = CType(total, Decimal)
        total = totalDec.ToString(fmt)
        '
        cadena = "?re=" & rfcE.ToString & "&rr=" & rfcR.ToString & "&tt=" & total.ToString & "&id=" & UUID.ToString
        '
        Response.Write(cadena)
        '   Genera gráfico
        '
        Dim qrCodeEncoder As New QRCodeEncoder
        qrCodeEncoder.QRCodeEncodeMode = qrCodeEncoder.ENCODE_MODE.BYTE
        qrCodeEncoder.QRCodeScale = 4
        qrCodeEncoder.QRCodeVersion = 8
        qrCodeEncoder.QRCodeErrorCorrect = qrCodeEncoder.ERROR_CORRECTION.Q
        Dim image As Drawing.Image

        image = qrCodeEncoder.Encode(cadena)
        image.Save(Server.MapPath("~/portalCFD/cbb") & "\" & serie.ToString & folio.ToString & ".png", System.Drawing.Imaging.ImageFormat.Png)
        '
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
            '
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try
        Return Total
    End Function

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
            End If
            rs.Close()
            '
        Catch ex As Exception
            '
            Response.Write(ex.ToString)
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




        Select Case tipoid
            Case 3, 6, 7      ' Recibo de honorarios y arrendamiento
                Dim reporte As New Formatos.formato_cfdi_honorarios
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 3
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Arrendamiento No.    " & serie.ToString & folio.ToString
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
                    Case 7
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noInterior", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "codigoPostal", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "pais", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "selloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara

                '
                If tipocontribuyenteid = 1 Then
                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
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
                        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                        reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva / 3) * 2, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - ((iva / 3) * 2), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva) - ((iva / 3) * 2), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                    Else
                        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
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
                        CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                    End If
                    
                End If

                

                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento()
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                Return reporte
            Case Else
                Dim reporte As New Formatos.formato_cfdi
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
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noInterior", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "codigoPostal", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "pais", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "selloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe, 2).ToString
                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString

                Select Case tasaid.SelectedValue
                    Case 2
                        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 11%"
                    Case 3
                        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    Case Else
                        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                End Select
                reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString

                reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento()
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                If tipoid = 5 Then
                    retencion = FormatNumber((importe * 0.04), 2)
                    reporte.ReportParameters("txtRetencion").Value = FormatCurrency(retencion, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
                    largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                    If divisaid = 1 Then
                        CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    Else
                        CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                    End If
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                End If
                Return reporte
        End Select
    End Function

    Private Function GeneraPDF_PreImpreso(ByVal cfdid As Long) As Telerik.Reporting.Report
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


        Dim ds As DataSet = New DataSet

        Try


            Dim cmd As New SqlCommand("EXEC pCFD  @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
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
                tipocontribuyenteid = rs("tipocontribuyenteid")
            End If
            rs.Close()
            '
        Catch ex As Exception
            '
            Response.Write(ex.ToString)
            '
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


        Select Case tipoid
            Case 3, 6, 7      ' Recibo de honorarios, Factura con retención de IVA
                Dim reporte As New Formatos.formato_cfdi_honorarios_preimpresa
                reporte.ReportParameters("cfdiId").Value = cfdid
                reporte.ReportParameters("plantillaId").Value = 1
                Select Case tipoid
                    Case 3
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Arrendamiento No.    " & serie.ToString & folio.ToString
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
                    Case 7
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noInterior", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "codigoPostal", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "pais", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "selloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara

                '
                If tipocontribuyenteid = 1 Then
                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
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
                        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                        reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva / 3) * 2, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - ((iva / 3) * 2), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva) - ((iva / 3) * 2), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                    Else
                        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
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
                        CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                    End If
                    
                End If

                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento()
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                Return reporte
            Case Else
                Dim reporte As New Formatos.formato_cfdi_preimpresa
                reporte.ReportParameters("cfdiId").Value = cfdid
                reporte.ReportParameters("plantillaId").Value = 1
                Select Case tipoid
                    Case 1, 4
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                    Case 2, 8
                        reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
                    Case 5
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "noInterior", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "codigoPostal", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "pais", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "selloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe, 2).ToString
                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento()
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                Select Case tasaid.SelectedValue
                    Case 2
                        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 11%"
                    Case 3
                        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    Case Else
                        reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                End Select
                '
                If tipoid = 5 Then
                    retencion = FormatNumber((importe * 0.04), 2)
                    reporte.ReportParameters("txtRetencion").Value = FormatCurrency(retencion, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
                    largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                    If divisaid = 1 Then
                        CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    Else
                        CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                    End If
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                End If
                Return reporte
        End Select
        '
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

    Private Function CadenaOriginalComplemento() As String

        '
        '   Obtiene los valores del timbre de respuesta
        '
        Dim selloSAT As String = ""
        Dim noCertificadoSAT As String = ""
        Dim selloCFD As String = ""
        Dim fechaTimbrado As String = ""
        Dim UUID As String = ""
        Dim Version As String = ""
        '
        '
        Dim s_RutaRespuestaPAC As String = Server.MapPath("cfd_storage") & "\" & "iu_timbre_" & serie.ToString & folio.ToString & ".xml"
        Dim respuestaPAC As New Timbrado()
        Dim objStreamReader As New StreamReader(s_RutaRespuestaPAC)
        Dim Xml As New XmlSerializer(respuestaPAC.[GetType]())
        respuestaPAC = DirectCast(Xml.Deserialize(objStreamReader), Timbrado)
        objStreamReader.Close()

        '
        'Crear el objeto timbre para asignar los valores de la respuesta PAC
        fechaTimbrado = respuestaPAC.Items(0).Informacion(0).Timbre(0).FechaTimbrado
        noCertificadoSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).noCertificadoSAT.ToString
        selloCFD = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloCFD.ToString
        selloSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloSAT.ToString
        UUID = respuestaPAC.Items(0).Informacion(0).Timbre(0).UUID.ToString
        Version = respuestaPAC.Items(0).Informacion(0).Timbre(0).version.ToString
        '
        Dim cadena As String = ""
        cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        Return cadena
        '
    End Function
#End Region

#Region "Telerik Autocomplete"

    Protected Sub cmbCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCode.TextChanged

        Dim codeSeparator As Integer
        codeSeparator = cmbCode.Text.IndexOf(":")

        Dim codeValue As String

        If codeSeparator <> -1 Then

            codeValue = cmbCode.Text.Substring(0, codeSeparator)

        Else

            codeValue = cmbCode.Text

        End If

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=7, @codigo='" & codeValue & "', @clienteprincipalid='" & Session("clienteid").ToString & "', @clienteid='" & cmbClient.SelectedValue.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                cmbCode.SelectedItem.Text = codeValue
                txtDescription.Text = rs("descripcion")
                txtUnitaryPrice.Text = rs("precio")
                txtUnit.Text = rs("unidad")
                lblSearchResult.Text = ""
                productoid.Value = rs("id")

            End If

        Catch ex As Exception
            txtDescription.Text = ex.ToString

        Finally

            conn.Close()

        End Try

        txtQuantity.Focus()

    End Sub

#End Region

    Protected Sub chkAduana_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAduana.CheckedChanged
        panelInformacionAduanera.Visible = chkAduana.Checked
        valNombreAduana.Enabled = chkAduana.Checked
        valFechaPedimento.Enabled = chkAduana.Checked
        valNumeroPedimento.Enabled = chkAduana.Checked
    End Sub


    Protected Sub tasaid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tasaid.SelectedIndexChanged
        tasaid.Enabled = False
    End Sub

    Protected Sub serieid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles serieid.SelectedIndexChanged
        serieid.Enabled = False
    End Sub
End Class
