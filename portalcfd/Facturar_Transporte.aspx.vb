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
Imports FirmaSAT.Sat
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Util
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Imports System.Globalization
Imports LinkiumCFDI.Cfdi32

Partial Class portalcfd_Facturar_Transporte
    Inherits System.Web.UI.Page
    Private importe As Decimal = 0
    Private iva As Decimal = 0
    Private total As Decimal = 0
    Private importetasacero As Decimal = 0
    Private maniobras As Decimal = 0
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
    Private FacturaXML As New Comprobante
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


            Dim ObjCat As New DataControl
            ObjCat.Catalogo(cmbClient, "EXEC pMisClientes @cmd=1, @clienteUnionId='" & Session("clienteid") & "' ", 0)
            'ObjCat.Catalogo(serieid, "select distinct isnull(a.serie,'Sin serie'), isnull(b.nombre,'Sin serie') from tblMisFolios a inner join tblTipoDocumento b on a.tipoid=b.id order by isnull(b.nombre,'Sin serie')", 0)
            ObjCat.Catalogo(serieid, "select id, isnull(nombre,'') as nombre from tblTipoDocumento order by nombre", 1)
            ObjCat.Catalogo(tasaid, "select id, nombre from tblTasa", 3)
            ObjCat.Catalogo(tipopagoid, "select id, nombre from tblTipopago", 1)

            ObjCat = Nothing

            cmbClient.Text = Resources.Resource.cmbEmptyMessage

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblContact.Text = Resources.Resource.lblContact
            lblContactPhone.Text = Resources.Resource.lblContactPhone
            lblRFC.Text = Resources.Resource.lblRFC
            lblEnviar.Text = Resources.Resource.lblEnviarA
            lblMetodoPago.Text = Resources.Resource.lblMetodoPago
            lblNumCtaPago.Text = Resources.Resource.lblNumCtaPago
            lblNumCtaPago.ToolTip = Resources.Resource.lblNumCtaPagoTooltip


            lblSubTotal.Text = Resources.Resource.lblSubTotal
            lblIVA.Text = Resources.Resource.lblIVA
            lblTotal.Text = Resources.Resource.lblTotal

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




            If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
                panelDivisas.Visible = True
            Else
                panelDivisas.Visible = False
            End If

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

                cmbClient.SelectedValue = rs("clienteid")
                tasaid.SelectedValue = rs("tasaid")
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
        ''
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
                txtLugarExpedicion.Text = rs("expedicionLinea3")
            End If

            rs.Close()

        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        ''
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
                lblTipoPrecioValue.Text = rs("tipoprecio")
                tipoprecioid = rs("tipoprecioid")
                Dim ObjData As New DataControl
                ObjData.Catalogo(formapagoid, "select id, nombre from tblFormaPago", rs("formapagoid"))
                ObjData.Catalogo(condicionesId, "select id, nombre from tblCondiciones", rs("condicionesid"))
                ObjData = Nothing
                txtNumCtaPago.Text = rs("numctapago")
                tipocontribuyenteid = rs("tipocontribuyenteid")
            End If
            '


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



#End Region

#Region "Add Invoice Items"

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

    Protected Sub InsertItem(ByVal id As Integer, ByVal item As GridItem)
        '
        ' Instancía objetos del grid
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim txtDescripcion As System.Web.UI.WebControls.TextBox = DirectCast(item.FindControl("txtDescripcion"), System.Web.UI.WebControls.TextBox)
        Dim lblUnidad As Label = DirectCast(item.FindControl("lblUnidad"), Label)
        Dim txtQuantity As RadNumericTextBox = DirectCast(item.FindControl("txtQuantity"), RadNumericTextBox)
        Dim txtUnitaryPrice As RadNumericTextBox = DirectCast(item.FindControl("txtUnitaryPrice"), RadNumericTextBox)
        Dim ItemChkDescuento As System.Web.UI.WebControls.CheckBox = DirectCast(item.FindControl("ItemChkDescuento"), System.Web.UI.WebControls.CheckBox)

        Dim descuentobit As Integer = 0
        If ItemChkDescuento.Checked = True Then
            descuentobit = 1
        Else
            descuentobit = 0
        End If
        '
        '   Agrega la partida
        '
        Dim objdata As New DataControl
        objdata.RunSQLQuery("EXEC pCFD @cmd=2, @cfdid='" & Session("CFD").ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & txtDescripcion.Text & "', @cantidad='" & txtQuantity.Text & "', @unidad='" & lblUnidad.Text & "', @precio='" & txtUnitaryPrice.Text & "', @productoid='" & id.ToString & "', @descuentobit='" & descuentobit.ToString & "', @tasaid='" & tasaid.SelectedValue.ToString & "'")
        objdata = Nothing
        '
        ''
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
            '
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub CargaTotales()


        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=16, @cfdid='" & Session("CFD").ToString & "', @tipocambio='" & tipocambio.Text & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "'", conn)
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
                importetasacero = rs("importetasacero")
                maniobras = rs("maniobras")

                '
                lblSubTotalValue.Text = FormatCurrency(rs("importe_pesos"), 2).ToString
                lblManiobrasValue.Text = FormatCurrency(rs("maniobras"), 2).ToString
                lblDescuentoValue.Text = FormatCurrency(totaldescuento, 2).ToString
                lblIVAValue.Text = FormatCurrency(rs("iva_pesos"), 2).ToString
                lblTotalValue.Text = FormatCurrency(rs("total_pesos"), 2).ToString
                '
                '
                Select Case tipoid
                    Case 3, 6
                        '
                        If tipocontribuyenteid <> 1 Then
                            lblRetIVAValue.Text = FormatCurrency((iva / 3) * 2, 2).ToString
                            lblRetISRValue.Text = FormatCurrency((importe * 0.1), 2).ToString
                            lblTotalValue.Text = FormatCurrency((total - (importe * 0.1) - ((iva / 3) * 2)), 2).ToString
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If
                        '
                    Case 7
                        '
                        If tipocontribuyenteid <> 1 Then
                            lblRetIVAValue.Text = FormatCurrency((iva * 0.1), 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                            lblTotalValue.Text = FormatCurrency((total - (iva * 0.1)), 2).ToString
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If
                        '
                    Case Else
                        lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                        lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                End Select
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
                e.Item.Cells(1).Text = Replace(e.Item.DataItem("descripcion"), vbCrLf, "<br />").ToString
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
        '   Agregado para remisionar
        '
        '
        If serieid.SelectedValue = 10 Then
            Call Remisionar()
        Else
            '
            Call CargaTotales()
            '
            '   Rutina de generación de XML CFDI Versión 3.2
            '
            FacturaXML.version = "3.2"
            FacturaXML.fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))
            FacturaXML.formaDePago = tipopagoid.SelectedItem.Text
            '
            If condicionesId.SelectedIndex = 0 Then
                FacturaXML.condicionesDePago = "Contado"
            Else
                FacturaXML.condicionesDePago = condicionesId.SelectedItem.Text
            End If

            FacturaXML.subTotal = importe
            FacturaXML.total = total
            If totaldescuento > 0 Then
                FacturaXML.descuento = totaldescuento
                FacturaXML.descuentoSpecified = True
            End If

            '
            '   Si es factura en dólares cambia la moneda y tipo de cambio
            '
            If (tipoid = 4 Or tipoid = 8) Then
                '
                FacturaXML.Moneda = "USD"
                FacturaXML.TipoCambio = tipocambio.Text
                '
            Else
                FacturaXML.Moneda = "MXN"
            End If
            '
            '   Verifica que tipo de comprobante se va a emitir
            '
            Select Case tipoid
                Case 1, 3, 4, 5, 6
                    '
                    FacturaXML.tipoDeComprobante = ComprobanteTipoDeComprobante.ingreso
                    '
                Case 2, 8   '   Nota de Crédito
                    '
                    FacturaXML.tipoDeComprobante = ComprobanteTipoDeComprobante.egreso
            End Select
            '
            FacturaXML.metodoDePago = formapagoid.SelectedItem.Text
            If txtNumCtaPago.Text.Length > 0 Then
                FacturaXML.NumCtaPago = txtNumCtaPago.Text
            End If
            FacturaXML.LugarExpedicion = txtLugarExpedicion.Text
            '
            '   Agrega los datos del emisor
            '
            Call ConfiguraEmisor()
            '
            '
            '   Asigna los datos del receptor
            '
            Call ConfiguraReceptor()
            '
            '   Agrega los conceptos de la factura
            '
            Call AgregaConceptos()
            '
            '   Asigna Serie y Folio
            '
            Call AsignaSerieFolio()
            '
            '   Agrega Impuestos
            '
            Call AgregaImpuestos()
            '
            'Crear cadena original
            '
            Dim otrasRutinas As New RutinasCFDI32
            Dim cadenaOriginal As String = otrasRutinas.GenerarCadenaV3(FacturaXML)
            '
            '   Generar Sello digital
            '
            '   Obtiene llave y contraseña
            '
            Call obtienellave()
            '
            '
            '   Lectura del certificado de sello digital
            '
            Dim cCert As New X509Certificate()
            Dim strSerial As String = String.Empty
            cCert = X509Certificate.CreateFromCertFile(archivoCertificado)
            '
            Dim i As Integer
            Dim sn As String = cCert.GetSerialNumberString()
            For i = 0 To sn.Length - 1
                If i Mod 2 <> 0 Then
                    strSerial = strSerial & sn.Substring(i, 1)
                End If
            Next i

            FacturaXML.noCertificado = strSerial
            FacturaXML.certificado = Convert.ToBase64String(cCert.GetRawCertData())

            '
            FacturaXML.sello = otrasRutinas.GenerarSelloDigital(archivoLlavePrivada, contrasenaLlavePrivada, cadenaOriginal)
            '
            FacturaXML.SaveToFile(Server.MapPath("cfd_storage") & "\" & "iu_" & serie.ToString & folio.ToString & ".xml", System.Text.Encoding.UTF8)
            '
            '   Realiza Timbrado
            '
            If TimbradoFacturaxion() = True Then

                '
                '   Si hay addenda la agrega al XML
                '
                'If chkAddenda.Checked = True Then
                '    Call AgregaAddendaAHMSA(Session("CFD"), serie.ToString, folio)
                'End If
                '
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
                '   Descarga Inventario si hay folio y fué timbrado el cfdi
                '
                If System.Configuration.ConfigurationManager.AppSettings("inventarios") = 1 Then
                    Call DescargaInventario(Session("CFD"))
                End If
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
        End If
        '


        Dim cfdid As Long = 0
        cfdid = Session("CFD")
        '
        '
        '   Verifica timbrado y rescate de folio
        '
        Call VerificaTimbrado(cfdid)
        '
        '
        If System.Configuration.ConfigurationManager.AppSettings("usuarios") = 1 Then
            Call AsignaCFDUsuario(cfdid)
        End If
        '
        Session("CFD") = 0
        '
        Response.Redirect("~/portalcfd/cfd.aspx")
        ''
    End Sub

    Private Sub DescargaInventario(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pInventario @cmd=7, @cfdid='" & cfdid.ToString & "', @userid='" & Session("userid").ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub VerificaTimbrado(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pCFD @cmd=32, @cfdid='" & cfdid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub AgregaAddendaAHMSA(ByVal cfdid As Long, ByVal serie As String, ByVal folio As Long)
        '
        '   Guarda addenda en la BD
        '
        '
        'Dim fi As String = ""
        'Dim ff As String = ""
        'If adFechainicio.SelectedDate.HasValue Then
        '    fi = adFechainicio.SelectedDate.Value.ToString("dd.MM.yyyy")
        'End If
        'If adFechaFin.SelectedDate.HasValue Then
        '    ff = adFechaFin.SelectedDate.Value.ToString("dd.MM.yyyy")
        'End If
        ''
        'Dim ObjData As New DataControl
        'ObjData.RunSQLQuery("exec pAddendaAHMSA @cmd=1, @cfdid='" & cfdid.ToString & "', @version='1.0', @tipo='" & adTipo.SelectedValue.ToString & "', @clase='" & adClase.SelectedValue.ToString & "', @NumSociedad='" & adNumsociedad.SelectedValue.ToString & "', @NumDivision='" & adNumdivision.SelectedValue.ToString & "', @NumProveedor='" & adNumProveedor.Text & "', @Correo='" & adCorreo.Text & "', @Moneda='" & adMoneda.SelectedValue.ToString & "', @Pedido='" & adPedido.Text & "', @Recepcion='" & adRecepcion.Text & "', @HojaServicio='" & adHojaServicio.Text & "', @Transporte='" & adTransporte.Text & "', @CtaxPag='" & adCtaxPag.Text & "', @Ejercicio='" & adEjercicio.Text & "', @FechaInicio='" & fi.ToString & "', @FechaFin='" & ff.ToString & "'")
        'ObjData = Nothing
        ''
        ''   Genera nodo de XML
        ''
        'Dim adendaAHM As New uAddendas.AHM.AddendaAHM()
        'adendaAHM.Version = "1.0"

        'Dim documentoAHM As New uAddendas.AHM.AddendaAHMDocumento()
        'documentoAHM.Tipo = adTipo.SelectedValue
        'documentoAHM.Clase = adClase.SelectedValue


        'Dim encabezadoAHM As New uAddendas.AHM.AddendaAHMDocumentoEncabezado()
        'encabezadoAHM.NumSociedad = adNumsociedad.SelectedValue
        'encabezadoAHM.NumDivision = adNumdivision.SelectedValue
        'encabezadoAHM.NumProveedor = adNumProveedor.Text
        'encabezadoAHM.Correo = adCorreo.Text
        'encabezadoAHM.Moneda = adMoneda.SelectedValue


        ''Agregar encabezado
        'documentoAHM.Encabezado = encabezadoAHM

        'Dim detalle As New uAddendas.AHM.AddendaAHMDocumentoDetalle()

        'If adPedido.Text.Length > 0 Then

        '    Dim pedido As New uAddendas.AHM.AddendaAHMDocumentoDetallePedido()
        '    pedido.Num = adPedido.Text
        '    If adRecepcion.Text.Length > 0 Then
        '        Dim reciboitem As String = adRecepcion.Text
        '        Dim recepcionLst As New List(Of Object)
        '        recepcionLst.Add(reciboitem)
        '        pedido.Recepcion = recepcionLst
        '    End If
        '    'Agregar pedido
        '    detalle.Pedido.Add(pedido)
        'End If


        'If adHojaServicio.Text.Length > 0 Then
        '    Dim hservicio As New uAddendas.AHM.AddendaAHMDocumentoDetalleHojaServicio()
        '    hservicio.Num = adHojaServicio.Text
        '    'Agregar hoja de servicio
        '    detalle.HojaServicio = hservicio
        'End If


        'If adTransporte.Text.Length > 0 Then
        '    Dim transporte As New uAddendas.AHM.AddendaAHMDocumentoDetalleTransporte()
        '    transporte.Num = adTransporte.Text
        '    'Agregar transporte
        '    detalle.Transporte = transporte
        'End If

        'If adCtaxPag.Text.Length > 0 Then
        '    Dim ctaPagar As New uAddendas.AHM.AddendaAHMDocumentoDetalleCtaxPag()
        '    ctaPagar.Ejercicio = adEjercicio.Text
        '    ctaPagar.Num = adCtaxPag.Text
        '    'Agregar Cuenta por pagar
        '    detalle.CtaxPag = ctaPagar
        'End If


        'If adFechainicio.SelectedDate.HasValue Or adFechaFin.SelectedDate.HasValue Then

        '    Dim liquida As New uAddendas.AHM.AddendaAHMDocumentoDetalleLiquidacion()

        '    If adFechainicio.SelectedDate.HasValue Then
        '        liquida.FechaInicio = adFechainicio.SelectedDate.Value.ToString("dd.MM.yyyy")
        '    End If
        '    '
        '    If adFechaFin.SelectedDate.HasValue Then
        '        liquida.FechaFin = adFechaFin.SelectedDate.Value.ToString("dd.MM.yyyy")
        '    End If
        '    'Agregar liquidación
        '    detalle.Liquidacion = liquida
        'End If

        ''Agregar detalle
        'documentoAHM.Detalle = detalle

        ''Dim anexo1 As String = "Factura1.pdf"
        ''documentoAHM.Anexos.Add(anexo1)

        ''Agregar documento al Objeto Adenda
        'adendaAHM.Documento = documentoAHM


        'Dim addenda As New Cfdi32.ComprobanteAddenda()

        'Dim resultadoAdenda As String = adendaAHM.Serialize()

        'Dim xmlDoc As System.Xml.XmlDocument = New XmlDocument()
        'xmlDoc.LoadXml(resultadoAdenda)

        'Dim elementoAddenda As XmlElement = xmlDoc.DocumentElement
        'addenda.Any = New XmlElement() {elementoAddenda}


        'FacturaXML.Addenda = addenda
        ''
        ''
    End Sub

    Private Sub AsignaCFDUsuario(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pUsuarios @cmd=7, @userid='" & Session("userid").ToString & "', @cfdid='" & cfdid.ToString & "'")
        ObjData = Nothing
    End Sub


    Private Sub Remisionar()
        '
        Call CargaTotales()
        '


        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '

        '
        '   Obtiene folio y actualiza cfd
        '
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""
        Dim tipoid As Integer = 0

        Dim SQLUpdate As String = ""

        If Not chkAduana.Checked Then
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @enviara='" & enviara.Text & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='', @numero_pedimento='" & numeropedimento.Text & "', @tipocambio='" & tipocambio.Text & "', @fecha_factura='" & Now.ToShortDateString & "', @tipopagoid='" & tipopagoid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @lugarexpedicion='" & txtLugarExpedicion.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
        Else
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @enviara='" & enviara.Text & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='" & fechapedimento.SelectedDate.Value.ToShortDateString & "', @numero_pedimento='" & numeropedimento.Text & "', @tipocambio='" & tipocambio.Text & "', @fecha_factura='" & Now.ToShortDateString & "', @tipopagoid='" & tipopagoid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @lugarexpedicion='" & txtLugarExpedicion.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
        End If

        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand(SQLUpdate, connF)
        'Try

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
        'Catch ex As Exception
        'txtDescription.Text = ex.ToString
        'Finally
        connF.Close()
        connF.Dispose()
        connF = Nothing
        ' End Try
        '
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
        If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & ".pdf") Then
            GuardaPDF(GeneraPDF_Documento(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & ".pdf")
        End If
        '
        '
        '   Genera PDF PreImpreso
        '
        If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & "_preimpreso.pdf") Then
            GuardaPDF(GeneraPDF_Documento_Preimpreso(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & "_preimpreso.pdf")
        End If
        ''
    End Sub


    Private Sub AsignaSerieFolio()
        '
        '   Obtiene serie y folio
        '
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""

        Dim SQLUpdate As String = ""

        If Not chkAduana.Checked Then
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @enviara='" & enviara.Text & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='', @numero_pedimento='" & numeropedimento.Text & "', @tipocambio='" & tipocambio.Text & "', @formapagoId='" & formapagoid.SelectedValue.ToString & "', @tipopagoId='" & tipopagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
        Else
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @enviara='" & enviara.Text & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='" & fechapedimento.SelectedDate.Value.ToShortDateString & "', @numero_pedimento='" & numeropedimento.Text & "', @tipocambio='" & tipocambio.Text & "', @formapagoId='" & formapagoid.SelectedValue.ToString & "', @tipopagoId='" & tipopagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
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
            '
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
        ''
    End Sub

    Private Sub ConfiguraEmisor()
        '
        '   Obtiene datos del emisor
        '
        '
        '   Datos del Emisor
        '
        Dim Emisor As New ComprobanteEmisor()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=11", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                Emisor.rfc = rs("fac_rfc")
                Emisor.nombre = rs("razonsocial")

                'Domicilio Fiscal
                Dim domicilioEmisor As New t_UbicacionFiscal()
                domicilioEmisor.calle = rs("fac_calle")
                domicilioEmisor.codigoPostal = rs("fac_cp")
                domicilioEmisor.colonia = rs("fac_colonia")
                domicilioEmisor.estado = rs("fac_estado")
                domicilioEmisor.localidad = rs("fac_municipio")
                domicilioEmisor.municipio = rs("fac_municipio")
                domicilioEmisor.noExterior = rs("fac_num_ext")
                If rs("fac_num_int").ToString.Length > 0 Then
                    domicilioEmisor.noInterior = rs("fac_num_int")
                End If
                domicilioEmisor.pais = "México"

                'Expedido En (Aplica cuando se trata de una sucursal)
                'Dim expedidoEn As New t_Ubicacion()
                'expedidoEn.calle = "Jacinto Lopez"
                'expedidoEn.codigoPostal = "85000"
                'expedidoEn.colonia = "Cortinas"
                'expedidoEn.estado = "Sonora"
                'expedidoEn.localidad = "Obregon"
                'expedidoEn.municipio = "Cajeme"
                'expedidoEn.noExterior = "100"
                'expedidoEn.noInterior = "A"
                'expedidoEn.pais = "México"
                'Asignar el expedidoEn
                'Emisor.ExpedidoEn = expedidoEn
                'Asignar el domicilio al emisor
                Emisor.DomicilioFiscal = domicilioEmisor
                '
                '
                '   Régimen fiscal. Es obligatorio y debe tener al menos 1
                '
                Dim regimenFiscal1 As New ComprobanteEmisorRegimenFiscal()
                regimenFiscal1.Regimen = rs("regimen")
                '
                '   Asignar el regimen fiscal dentro del emisor
                Emisor.RegimenFiscal = New ComprobanteEmisorRegimenFiscal() {regimenFiscal1}
                '
                '   Asignar el emisor al CFDI
                '
                FacturaXML.Emisor = Emisor
                '
                '
            End If

        Catch ex As Exception
            '
        Finally
            conn.Close()
        End Try
        '
        ''
    End Sub

    Private Sub ConfiguraReceptor()
        '
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

                Dim Receptor As New ComprobanteReceptor()
                Receptor.rfc = rs("fac_rfc")
                Receptor.nombre = rs("razonsocial")

                'Crear domicilio del receptor
                Dim domicilioReceptor As New t_Ubicacion()
                domicilioReceptor.calle = rs("fac_calle")
                domicilioReceptor.codigoPostal = rs("fac_cp")
                domicilioReceptor.colonia = rs("fac_colonia")
                domicilioReceptor.estado = rs("fac_estado")
                domicilioReceptor.localidad = rs("fac_municipio")
                domicilioReceptor.municipio = rs("fac_municipio")
                domicilioReceptor.noExterior = rs("fac_num_ext")
                If rs("fac_num_int").ToString.Length > 0 Then
                    domicilioReceptor.noInterior = rs("fac_num_int")
                End If
                domicilioReceptor.pais = rs("fac_pais")
                '
                '   Asignar el domiclio al receptor
                '
                Receptor.Domicilio = domicilioReceptor
                '
                '   Asignar el Receptor al CFD
                '
                FacturaXML.Receptor = Receptor
                '
                tipocontribuyenteid = rs("tipocontribuyenteid")
            End If

        Catch ex As Exception
            'txtDescription.Text = ex.ToString
        Finally
            connR.Close()
            connR.Dispose()
            connR = Nothing
        End Try
        '
        ''
    End Sub

    Private Sub AgregaConceptos()
        '
        '   Agrega Partidas
        '
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD @cmd=13, @cfdId='" & Session("CFD").ToString & "'", connP)
        Try
            connP.Open()
            '
            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()
            '
            Dim Concepto As ComprobanteConcepto
            Dim lstConceptos As New List(Of ComprobanteConcepto)
            While rs.Read
                Concepto = New ComprobanteConcepto
                Concepto.descripcion = rs("descripcion")
                Concepto.cantidad = rs("cantidad")
                Concepto.valorUnitario = Convert.ToDecimal(rs("precio"))
                Concepto.importe = Convert.ToDecimal(rs("importe"))
                Concepto.noIdentificacion = rs("codigo")
                Concepto.unidad = rs("unidad")
                '
                '
                If chkAduana.Checked = True Then
                    Dim itemAduanaData As New t_InformacionAduanera()
                    itemAduanaData.aduana = nombreaduana.Text
                    itemAduanaData.fecha = fechapedimento.SelectedDate.Value
                    itemAduanaData.numero = numeropedimento.Text
                    Concepto.Items = New t_InformacionAduanera() {itemAduanaData}
                End If
                '
                lstConceptos.Add(Concepto)
                '
            End While
            '
            FacturaXML.Conceptos = lstConceptos.ToArray()
            '
        Catch ex As Exception
            '
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try
        '
        ''
    End Sub

    Private Sub AgregaImpuestos()
        '
        '
        '   Agrega impuestos
        '
        '
        Dim ImpuestoTrasladadoIVA As New ComprobanteImpuestosTraslado()
        ImpuestoTrasladadoIVA.importe = Convert.ToDecimal(iva)
        ImpuestoTrasladadoIVA.impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA
        Select Case tasaid.SelectedValue
            Case 1
                ImpuestoTrasladadoIVA.tasa = 0
            Case 2
                ImpuestoTrasladadoIVA.tasa = 11
            Case 3
                ImpuestoTrasladadoIVA.tasa = 16
            Case Else
                ImpuestoTrasladadoIVA.tasa = 16
        End Select
        '
        '
        '
        '   Asigna los impuestos
        '
        Dim impuestos As New ComprobanteImpuestos()
        impuestos.Traslados = New ComprobanteImpuestosTraslado() {ImpuestoTrasladadoIVA}
        impuestos.totalImpuestosTrasladados = Convert.ToDecimal(iva)
        impuestos.totalImpuestosTrasladadosSpecified = True
        FacturaXML.Impuestos = impuestos
        '

        '
        '   Retenciones
        '
        Select Case tipoid
            Case 3, 6   '   Recibos de honorarios o arrendamiento
                '
                '   Retenciones
                '
                If tipocontribuyenteid = 1 Then
                    FacturaXML.total = FormatNumber(total, 4)
                Else
                    '
                    '   ISR
                    '
                    Dim RetencionISR As New ComprobanteImpuestosRetencion()
                    RetencionISR.importe = FormatNumber((importe * 0.1), 4)
                    RetencionISR.impuesto = ComprobanteImpuestosRetencionImpuesto.ISR
                    '
                    '   IVA
                    '
                    Dim RetencionIVA As New ComprobanteImpuestosRetencion()
                    RetencionIVA.importe = FormatNumber((iva / 3) * 2, 4)
                    RetencionIVA.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
                    '
                    FacturaXML.Impuestos.Retenciones = New ComprobanteImpuestosRetencion() {RetencionISR, RetencionIVA}
                    FacturaXML.total = FormatNumber((total - (importe * 0.1) - ((iva / 3) * 2)), 4)

                End If
            Case 7  ' Retención del 10%

                '   Retenciones
                '
                If tipocontribuyenteid = 1 Then
                    FacturaXML.total = FormatNumber(total, 4)
                Else
                    '
                    '   IVA
                    '
                    Dim RetencionIVA As New ComprobanteImpuestosRetencion()
                    RetencionIVA.importe = FormatNumber((iva * 0.1), 4)
                    RetencionIVA.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
                    '
                    FacturaXML.Impuestos.Retenciones = New ComprobanteImpuestosRetencion() {RetencionIVA}
                    FacturaXML.total = FormatNumber((total - ((iva * 0.1))), 4)
                    '
                    '
                End If
        End Select

        '   Retención de 4%
        '
        If System.Configuration.ConfigurationManager.AppSettings("retencion4") = 1 And tipoid = 5 Then
            '
            Dim Retencion As New ComprobanteImpuestosRetencion()
            Retencion.importe = importe * 0.04
            Retencion.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
            '
            FacturaXML.Impuestos.Retenciones = New ComprobanteImpuestosRetencion() {Retencion}
            FacturaXML.total = total - (importe * 0.04)
            '
        End If
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
            schemaLocation.Value = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd"
            doc.DocumentElement.SetAttributeNode(schemaLocation)

            Dim schemaLocation2 As XmlAttribute = doc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance")
            schemaLocation2.Value = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd"
            doc.DocumentElement.SetAttributeNode(schemaLocation2)

            xmlTextWriter.Close()

            Return doc
        Catch ex As Exception
            Throw New Exception("Error al generar XmlDocument, Error: " & ex.Message)
        End Try
    End Function

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

            Dim FILENAME As String = Server.MapPath("~/portalcfd/cfd_storage" & "\Output.txt")

            'Get a StreamWriter class that can be used to write to the file
            Dim objStreamWriter As StreamWriter
            objStreamWriter = File.AppendText(FILENAME)

            'Append the the end of the string, "A user viewed this demo at: "
            'followed by the current date and time
            objStreamWriter.WriteLine(params)

            'Close the stream
            objStreamWriter.Close()

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
            Dim timbre As New uCFDsLib.TimbreFiscalDigital
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
            Dim xs As New XmlSerializer(GetType(uCFDsLib.TimbreFiscalDigital))
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
        ''
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
        ''
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
        Dim maniobras As Decimal = 0
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
                maniobras = rs("maniobras")
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
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
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
                        reporte.ReportParameters("txtDocumento").Value = "Recibo Arrendamiento No.    " & serie.ToString & folio.ToString
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
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara

                '
                If tipocontribuyenteid = 1 Then
                    reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                    reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
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
                        reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
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
                        reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
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
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                Return reporte
            Case Else
                Dim reporte As New Formatos.formato_cfdi_paging_transporte
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
                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(maniobras, 2).ToString

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
                '
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                '
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
        Dim em_regimen As String = ""
        Dim importe As Decimal = 0
        Dim maniobras As Decimal = 0
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
        Dim tipopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""


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
                em_regimen = rs("regimen")
                razonsocial = rs("razonsocial")
                callenum = rs("callenum")
                colonia = rs("colonia")
                ciudad = rs("ciudad")
                rfc = rs("rfc")
                importe = rs("importe")
                maniobras = rs("maniobras")
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
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
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
                        reporte.ReportParameters("txtDocumento").Value = "Recibo Arrendamiento No.    " & serie.ToString & folio.ToString
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
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                '
                Return reporte
            Case Else
                Dim reporte As New Formatos.formato_cfdi_paging_transporte_preimpreso
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
                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(maniobras, 2).ToString
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
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                '
                Return reporte
        End Select
        ''
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


        Dim ds As DataSet = New DataSet

        ' Try


        Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
        conn.Open()
        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then
            serie = rs("serie")
            folio = rs("folio")
            'tipoid = rs("tipoid")
            tipoid = serieid.SelectedValue
            em_razonsocial = rs("em_razonsocial")
            em_callenum = rs("em_callenum")
            em_colonia = rs("em_colonia")
            em_ciudad = rs("em_ciudad")
            em_rfc = rs("em_rfc")
            em_regimen = rs("regimen")
            '
            rec_razonsocial = rs("rec_razonsocial")
            rec_callenum = rs("rec_callenum")
            rec_colonia = rs("rec_colonia")
            rec_ciudad = rs("rec_ciudad")
            rec_rfc = rs("rec_rfc")
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
        '
        'Catch ex As Exception
        '
        'esponse.Write(ex.ToString)
        ' Finally

        conn.Close()
        conn.Dispose()
        conn = Nothing
        ' End Try



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
        reporte.ReportParameters("txtFechaEmision").Value = Now.ToShortDateString

        Select Case tipoid
            Case 10
                reporte.ReportParameters("txtDocumento").Value = "Remisión No.    " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
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
        '
        '
        reporte.ReportParameters("txtInstrucciones").Value = instrucciones
        reporte.ReportParameters("txtPedimento").Value = pedimento
        reporte.ReportParameters("txtEnviarA").Value = enviara

        '
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
        '
        '   Ajusta cantidad con texto
        '
        total = FormatNumber((importe + iva), 2)
        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
        '
        CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"

        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        '
        '
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
        If porcentaje > 0 Then
            reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
        End If
        '
        '
        reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
        reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
        reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
        reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
        reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString

        '
        Return reporte
    End Function

    Private Function GeneraPDF_Documento_Preimpreso(ByVal cfdid As Long) As Telerik.Reporting.Report
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


        Dim ds As DataSet = New DataSet

        ' Try


        Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
        conn.Open()
        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then
            serie = rs("serie")
            folio = rs("folio")
            'tipoid = rs("tipoid")
            tipoid = serieid.SelectedValue
            em_razonsocial = rs("em_razonsocial")
            em_callenum = rs("em_callenum")
            em_colonia = rs("em_colonia")
            em_ciudad = rs("em_ciudad")
            em_rfc = rs("em_rfc")
            em_regimen = rs("regimen")
            '
            rec_razonsocial = rs("rec_razonsocial")
            rec_callenum = rs("rec_callenum")
            rec_colonia = rs("rec_colonia")
            rec_ciudad = rs("rec_ciudad")
            rec_rfc = rs("rec_rfc")
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
        '
        'Catch ex As Exception
        '
        'esponse.Write(ex.ToString)
        ' Finally

        conn.Close()
        conn.Dispose()
        conn = Nothing
        ' End Try



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


        Dim reporte As New Formatos.formato_cbb_preimpreso


        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("txtFechaEmision").Value = Now.ToShortDateString

        Select Case tipoid
            Case 10
                reporte.ReportParameters("txtDocumento").Value = "Remisión No.    " & serie.ToString & folio.ToString
                reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
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
        '
        '
        reporte.ReportParameters("txtInstrucciones").Value = instrucciones
        reporte.ReportParameters("txtPedimento").Value = pedimento
        reporte.ReportParameters("txtEnviarA").Value = enviara

        '
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
        '
        '   Ajusta cantidad con texto
        '
        total = FormatNumber((importe + iva), 2)
        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
        '
        CantidadTexto = "( Son " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"

        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        '
        '
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
        If porcentaje > 0 Then
            reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
        End If
        '
        '
        reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
        reporte.ReportParameters("txtFormaPago").Value = tipopago.ToString
        reporte.ReportParameters("txtMetodoPago").Value = formapago.ToString
        reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
        reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString

        '
        Return reporte
    End Function
#End Region

#Region "Telerik Autocomplete"


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
                Call CargaTotales()
                panelResume.Visible = True
                gridResults.Visible = False
                itemsList.Visible = True
                txtSearchItem.Text = ""
                txtSearchItem.Focus()
                btnCancelSearch.Visible = False
        End Select
    End Sub

    Protected Sub txtSearchItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchItem.TextChanged
        gridResults.Visible = True
        itemsList.Visible = False
        Dim objdata As New DataControl
        gridResults.DataSource = objdata.FillDataSet("exec pCFD @cmd=30, @txtSearch='" & txtSearchItem.Text & "', @clienteid='" & cmbClient.SelectedValue.ToString & "'")
        gridResults.DataBind()
        objdata = Nothing
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = True
    End Sub

    Protected Sub gridResults_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridResults.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim txtQuantity As RadNumericTextBox = DirectCast(e.Item.FindControl("txtQuantity"), RadNumericTextBox)
                Dim txtUnitaryPrice As RadNumericTextBox = DirectCast(e.Item.FindControl("txtUnitaryPrice"), RadNumericTextBox)

                txtQuantity.Text = "1"
                txtUnitaryPrice.Text = e.Item.DataItem("precio")
        End Select
    End Sub

    Protected Sub cmbClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        Call CargaCliente(cmbClient.SelectedValue)
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
        '
        ''
    End Sub

    Protected Sub chkAddenda_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddenda.CheckedChanged
        panelAddendaAHMSA.Visible = True
        '
        '   Catálogos de la addenda
        '
        Dim ObjCat As New DataControl
        ObjCat.Catalogo(adTipo, "select id, nombre from tblAddendaAHMSA_tipo", 0)
        ObjCat.CatalogoStr(adClase, "select id, nombre from tblAddendaAHMSA_clase order by nombre", 0)
        ObjCat.CatalogoStr(adNumsociedad, "select id, nombre from tblAddendaAHMSA_sociedad order by nombre", 0)
        ObjCat.CatalogoStr(adNumdivision, "select id, nombre from tblAddendaAHMSA_division order by nombre", 0)
        ObjCat.CatalogoStr(adMoneda, "select id, nombre from tblAddendaAHMSA_moneda", 0)
        ObjCat = Nothing
        '
        adEjercicio.Text = Year(Now()).ToString
        ''
    End Sub
End Class
