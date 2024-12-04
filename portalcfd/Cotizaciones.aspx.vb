Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Imports System.Xml
Imports FirmaSAT.Sat
Imports System.Net.Mail
Imports System.Xml.Serialization
Imports uCFDsLib
Imports uCFDsLib.v3
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Util
Imports Telerik.Reporting.Processing

Partial Public Class Cotizaciones
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Server.ScriptTimeout = 3600
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
            fha_ini.SelectedDate = Now()
            fha_fin.SelectedDate = Now()
            Dim ObjCat As New DataControl
            ObjCat.Catalogo(tipoid, "select id, isnull(nombre,'') as nombre from tblTipoDocumento where id=10 order by nombre", 10)
            ObjCat.Catalogo(clienteid, "exec pMisClientes @cmd=1", 0)
            ObjCat = Nothing
            tipoid.Enabled = False

            If Not String.IsNullOrEmpty(Request.QueryString("tipodocumentoid")) Then
                tipoid.SelectedValue = Request.QueryString("tipodocumentoid")
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("clienteid")) Then
                clienteid.SelectedValue = Request.QueryString("clienteid")
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("fhaini")) Then
                fha_ini.SelectedDate = Request.QueryString("fhaini")
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("fhafin")) Then
                fha_fin.SelectedDate = Request.QueryString("fhafin")
            End If

            Call MuestraLista()
            Call SetGridFilters()
        End If
    End Sub

    Private Sub MuestraLista()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        cfdlist.DataSource = ObjData.FillDataSet("exec pCFD @cmd=8, @tipodocumentoid='" & tipoid.SelectedValue.ToString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "'")
        cfdlist.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub
#End Region

#Region "Grid Handle"
    Protected Sub cfdlist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cfdlist.ItemCommand
        RadWindow1.VisibleOnPageLoad = False
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/portalcfd/AgregarCotizacion.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdXML"
                Call DownloadXML(e.CommandArgument)
            Case "cmdPDF"
                Call DownloadPDF(e.CommandArgument)
            Case "cmdSend"
                Call DatosEmail(e.CommandArgument)
            Case "cmdPDFsv"
                Response.Redirect("~/portalcfd/FacturaPDFsinValor.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdDelete"
                Dim ObjData As New DataControl
                ObjData.RunSQLQuery("exec pCFD @cmd=9, @cfdid='" & e.CommandArgument.ToString & "'")
                ObjData = Nothing
                Call MuestraLista()
            Case "cmdCancel"
                Call CancelaCFDI33(e.CommandArgument)
                Call MuestraLista()
            Case "cmdAcuse"
                Call VerAcuse(e.CommandArgument)
            Case "cmdAddenda"
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","})
                Dim cfdif As String = commandArgs(0)
                Dim formulario As String = commandArgs(1)
                CreateWindowScript(cfdif, formulario)
            Case "cmdFacturar"
                Call Facturar(e.CommandArgument)
        End Select
    End Sub

    Protected Sub cfdlist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cfdlist.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim lnkEdit As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
                'Dim lnkCancelar As LinkButton = CType(e.Item.FindControl("lnkCancelar"), LinkButton)
                'Dim lnkAcuse As LinkButton = CType(e.Item.FindControl("lnkAcuse"), LinkButton)
                Dim lnkBorrar As LinkButton = CType(e.Item.FindControl("lnkBorrar"), LinkButton)
                'Dim lnkXML As LinkButton = CType(e.Item.FindControl("lnkXML"), LinkButton)
                Dim lnkPDF As LinkButton = CType(e.Item.FindControl("lnkPDF"), LinkButton)
                Dim lblTimbrado As Label = CType(e.Item.FindControl("lblTimbrado"), Label)
                Dim imgTimbrado As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgTimbrado"), System.Web.UI.WebControls.Image)
                Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)
                Dim lnkFacturar As LinkButton = CType(e.Item.FindControl("lnkFacturar"), LinkButton)

                Dim esTimbrado As Boolean = e.Item.DataItem("folio") > 0
                imgTimbrado.Visible = esTimbrado

                If Not e.Item.DataItem("timbrado") Then
                    'lblTimbrado.Text = " "
                    'If e.Item.DataItem("folio") = 0 Then
                    '    lnkEdit.Enabled = True
                    'Else
                    '    lnkEdit.Enabled = False
                    'End If
                End If

                If esTimbrado And CBool(e.Item.DataItem("cotizacion_timbradabit")) Then
                    lnkEdit.Enabled = False
                Else
                    lnkEdit.Enabled = True
                End If

                If CBool(e.Item.DataItem("formatoBit")) Then
                    'lnkEdit.Enabled = Not e.Item.DataItem("formatoBit")
                    'lnkXML.Visible = Not e.Item.DataItem("formatoBit")
                    'lnkPDF.Enabled = e.Item.DataItem("formatoBit")
                    imgSend.Enabled = e.Item.DataItem("formatoBit")
                    'lnkCancelar.Visible = Not e.Item.DataItem("formatoBit")
                    lnkBorrar.Visible = Not e.Item.DataItem("formatoBit")


                End If

                If e.Item.DataItem("enviadoBit") = True Then
                    imgSend.ImageUrl = "~/portalcfd/images/envelopeok.jpg"
                    imgSend.ToolTip = "Enviado el " & e.Item.DataItem("fechaenvio").ToString
                End If

                lnkBorrar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un folio no timbrado. ¿Desea continuar?');")
                'lnkCancelar.Attributes.Add("onclick", "javascript:return confirm('Va a cancelar un cfdi. ¿Desea continuar?');")
                imgSend.Attributes.Add("onclick", "javascript:return confirm('Va a enviar por correo los archivos del cfdi. ¿Desea continuar?');")

                'If e.Item.DataItem("estatus") = "Aplicado" Or e.Item.DataItem("estatus") = "Cancelado" Then
                '    lnkCancelar.Visible = True
                '    If CBool(e.Item.DataItem("formatoBit")) Then
                '        lnkCancelar.Visible = Not e.Item.DataItem("formatoBit")
                '    End If
                'Else
                '    lnkCancelar.Visible = False
                'End If

                If e.Item.DataItem("serieid") = 10 Then
                    If CBool(e.Item.DataItem("cotizacion_timbradabit")) = False Then
                        lnkFacturar.Visible = True
                    Else
                        lnkFacturar.Visible = False
                    End If
                ElseIf e.Item.DataItem("serieid") = 11 Then
                    If CBool(e.Item.DataItem("remision_timbradabit")) = False Then
                        lnkFacturar.Visible = True
                    Else
                        lnkFacturar.Visible = False
                    End If
                End If

                If e.Item.DataItem("folio_cfd_remision").ToString = "0" Then
                    e.Item.Cells(8).Text = ""
                End If

        End Select
    End Sub

    Protected Sub cfdlist_PageIndexChanged(ByVal source As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles cfdlist.PageIndexChanged
        cfdlist.CurrentPageIndex = e.NewPageIndex
        Call MuestraLista()
    End Sub

    Protected Sub cfdlist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles cfdlist.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        cfdlist.DataSource = ObjData.FillDataSet("exec pCFD @cmd=8, @tipodocumentoid='" & tipoid.SelectedValue.ToString & "', @clienteid='" & clienteid.SelectedValue.ToString & "', @fhaini='" & fha_ini.SelectedDate.Value.ToShortDateString & "', @fhafin='" & fha_fin.SelectedDate.Value.ToShortDateString & "'")
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        ''
    End Sub
#End Region

#Region "Functions"

    Private Sub DownloadXML(ByVal cfdid As Long)
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie = rs("serie").ToString
                folio = rs("folio").ToString
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
        ''
    End Sub

    Private Sub DownloadPDF(ByVal cfdid As Long)
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim tipoid As Integer = 0

        ' Manejo de la conexión SQL con Using
        Using connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
            Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
            Try
                connF.Open()

                Dim rs As SqlDataReader = cmdF.ExecuteReader()
                If rs.Read Then
                    tipoid = rs("tipoid")
                    serie = rs("serie").ToString()
                    folio = rs("folio").ToString()
                End If
            Catch ex As Exception
                ' Manejo de errores: Log o notificación
                ' Ejemplo: LogError(ex)
            End Try
        End Using

        ' Construir la ruta del archivo PDF
        Dim filePath = Server.MapPath("~/portalcfd/pdf/") & "link_" & serie.ToString() & folio.ToString() & ".pdf"
        If File.Exists(filePath) Then
            Dim fileName As String = Path.GetFileName(filePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & fileName & """")
            Response.Flush()
            Response.WriteFile(filePath)
            Response.End()
        Else
            ' Si el archivo no existe, genera el PDF
            Dim reportBook As New Telerik.Reporting.ReportBook()

            ' Agregar informes al ReportBook
            reportBook.Reports.Add(GeneraPDF_parte1(cfdid))
            reportBook.Reports.Add(GeneraPDF_parte2(cfdid))
            reportBook.Reports.Add(GeneraPDF_images(cfdid))
            reportBook.Reports.Add(GeneraPDF_parte3(cfdid))
            'reportBook.Reports.Add(GeneraPDFparte3())

            ' Renderizar el ReportBook como PDF
            Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
            Dim result As RenderingResult = reportProcessor.RenderReport("PDF", reportBook, Nothing)

            ' Guardar el PDF unido en el servidor
            Dim outputFilePath As String = Server.MapPath("~/portalcfd/pdf/") & "link_" & serie.ToString() & folio.ToString() & ".pdf"
            Using fs As New FileStream(outputFilePath, FileMode.Create)
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
            End Using

            ' Descargar el archivo generado
            Dim fileName As String = Path.GetFileName(outputFilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & fileName & """")
            Response.Flush()
            Response.WriteFile(outputFilePath)
            Response.End()
        End If
    End Sub


    Private Sub SendEmail(ByVal cfdid As Long)
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim tipoid As Integer = 0
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rsF = cmdF.ExecuteReader

            If rsF.Read Then
                serie = rsF("serie").ToString
                folio = rsF("folio").ToString
                tipoid = rsF("tipoid")
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        '
        '
        '
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim correo As String = ""
        Dim email_from As String = ""
        Dim email_smtp_server As String = ""
        Dim email_smtp_username As String = ""
        Dim email_smtp_password As String = ""
        Dim email_smtp_port As String = ""

        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pEnviaEmail @cfdid='" & cfdid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '       
            razonsocial = rs("razonsocial")
            correo = rs("email_to")
            email_from = rs("email_from")
            email_smtp_server = rs("email_smtp_server")
            email_smtp_username = rs("email_smtp_username")
            email_smtp_password = rs("email_smtp_password")
            email_smtp_port = rs("email_smtp_port")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        '
        mensaje = "<html><head></head><body><br />"

        If tipoid = 1 Then
            mensaje += "Estimado(a) Cliente, por este medio le hago llegar su Comprobante Fiscal Digital en formato PDF y en formato XML. Ud. puede imprimir el PDF e integrarlo a su contabilidad de forma tradicional o puede guardar el archivo XML que es fiscalmente válido."
        ElseIf tipoid = 10 Then
            mensaje += "Estimado(a) Cliente, por este medio le hago llegar la cotización solicitada en formato PDF. Ud. puede imprimir el PDF para su revisión. Gracias."
        End If


        mensaje += "<br /><br />"
        mensaje += "Atentamente.<br /><br />"
        mensaje += "<strong>" & razonsocial.ToString & "</strong><br /><br /></body></html>"


        Dim objMM As New MailMessage
        objMM.To.Add(correo)
        objMM.To.Add(email_from)
        objMM.From = New MailAddress(email_from, "OSDAN - Atención a clientes")
        objMM.IsBodyHtml = True
        objMM.Priority = MailPriority.Normal
        objMM.Subject = razonsocial & " - Comprobante Fiscal Digital"
        objMM.Body = mensaje
        '
        '   Agrega anexos
        '
        Dim AttachXML As Net.Mail.Attachment = New Net.Mail.Attachment(Server.MapPath("~/portalcfd/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml")
        Dim AttachPDF As Net.Mail.Attachment = New Net.Mail.Attachment(Server.MapPath("~/portalcfd/pdf/") & "link_" & serie.ToString & folio.ToString & ".pdf")
        '
        objMM.Attachments.Add(AttachXML)
        objMM.Attachments.Add(AttachPDF)
        '
        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = email_smtp_username
            SmtpUser.Password = email_smtp_password
            SmtpUser.Domain = email_smtp_server
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = email_smtp_server
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpMail.Send(objMM)
            '
            '   Lo marca como enviado
            Dim ObjData As New DataControl
            ObjData.RunSQLQuery("exec pCFD @cmd=40, @cfdid='" & cfdid.ToString & "'")
            ObjData = Nothing
            '
        Catch ex As Exception
            '
            '
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing
        '
        '
        Call MuestraLista()
        '
        ''
    End Sub

    Private Sub CancelaCFDI33(ByVal cfdi As Long)
        
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

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
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

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

    Private Function GeneraPDF(ByVal cfdid As Long, ByVal serie As String, ByVal folio As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim tipocontribuyenteid As Integer = 0
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
        Dim tasaid As Integer = 0


        Dim ds As DataSet = New DataSet

        Try


            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                tasaid = rs("tasaid")
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
            Response.End()
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
            Case 3, 5, 6, 7, 13      ' Recibo de honorarios y arrendamiento
                Dim reporte As New Formatos.formato_cfdi_honorarios
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 3
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Arrendamiento No.    " & serie.ToString & folio.ToString
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Recibo de Honorarios No.    " & serie.ToString & folio.ToString
                    Case 5, 7
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noInterior", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "codigoPostal", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "pais", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "selloSAT", "tfd:TimbreFiscalDigital")
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
                    If tipoid = 5 Then ' Retención del 4% Carta Porte
                        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                        reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        retencion = FormatNumber((importe * 0.04), 2)
                        reporte.ReportParameters("txtRetIva").Value = FormatCurrency(retencion, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
                        largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                        If divisaid = 1 Then
                            CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        Else
                            CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                        End If
                        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                        '
                    ElseIf tipoid = 7 Then
                        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                        reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                        reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva / 3) * 2, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - ((iva / 3) * 2), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva) - ((iva * 0.1)), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        If divisaid = 1 Then
                            CantidadTexto = "( SON " + Num2Text(total - decimales) & " PESOS " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        Else
                            CantidadTexto = "( SON " + Num2Text(total - decimales) & " DÓLARES " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
                        End If
                        '
                    ElseIf tipoid = 13 Then ' Retención del 16%
                        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
                        reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                        'reporte.ReportParameters("txtRetIva").Value = FormatCurrency((iva / 3) * 2, 2).ToString
                        'reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva) - ((iva / 3) * 2), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        retencion = FormatNumber((importe * 0.16), 2)
                        reporte.ReportParameters("txtRetIva").Value = FormatCurrency(retencion, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
                        largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                        If divisaid = 1 Then
                            CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        Else
                            CantidadTexto = "( Son " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                        End If
                        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
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
                        If divisaid = 1 Then
                            CantidadTexto = "( SON " + Num2Text(total - decimales) & " PESOS " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        Else
                            CantidadTexto = "( SON " + Num2Text(total - decimales) & " DÓLARES " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
                        End If
                        '
                    End If

                End If

                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(serie, folio)
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
                Dim reporte As New Formatos.formato_cfdi_paging
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
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "fecha", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "calle", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noExterior", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "noInterior", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteColonia").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "colonia", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "codigoPostal", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteCiudadEstado").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "municipio", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "estado", "cfdi:Domicilio") & " " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "pais", "cfdi:Domicilio")
                reporte.ReportParameters("txtClienteRFC").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
                '
                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "selloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtPedimento").Value = pedimento
                reporte.ReportParameters("txtEnviarA").Value = enviara
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe, 2).ToString
                reporte.ReportParameters("txtTasaCero").Value = FormatCurrency(importetasacero, 2).ToString

                Select Case tasaid
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
                reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(serie, folio)
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

    Public Function GeneraPDF_parte1(ByVal cfdid As Long) As Telerik.Reporting.Report
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
        Dim tipocontribuyenteid As Integer = 0

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

        Dim reporte As New formato_cotizacion_parte1
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("txtFechaEmision").Value = Now.ToShortDateString & TimeOfDay.ToString("h:mm:ss tt")
        reporte.ReportParameters("txtFechaDia").Value = Now.ToString("dd") ' Solo el día
        reporte.ReportParameters("txtFechaMes").Value = Now.ToString("MMMM") ' Nombre completo del mes
        reporte.ReportParameters("txtFechaYear").Value = Now.ToString("yyyy") ' Solo el año completo
        reporte.ReportParameters("txtFechaYear2").Value = Now.ToString("yy") ' Solo los últimos dos dígitos del año





        reporte.ReportParameters("paramImgLogo1").Value = Server.MapPath("~/portalcfd/logos/udara.jpeg")
        reporte.ReportParameters("paramImgLogo2").Value = Server.MapPath("~/portalcfd/logos/logo_carroll.jpeg")
        reporte.ReportParameters("paramImgLogo3").Value = Server.MapPath("~/portalcfd/logos/logo_CarrollAir.jpeg")
        reporte.ReportParameters("paramImgLogo4").Value = Server.MapPath("~/portalcfd/logos/logo_fst.jpeg")
        reporte.ReportParameters("txtDocumento").Value = "Cotización No. " & serie.ToString & folio.ToString


        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
        'Select Case tipoid
        '    Case 7  '   Pedido
        '        reporte.ReportParameters("txtDocumento").Value = "Pedido No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: SEÑALAR FECHA DE ENTREGA_________________________________"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 8
        '        reporte.ReportParameters("txtDocumento").Value = "Pago No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: GUARDAR COMPROBANTE PARA CUALQUIER ACLARACIÓN"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 9
        '        reporte.ReportParameters("txtDocumento").Value = "Orden de Compra No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: FAVOR DE ANEXAR ORDEN DE COMPRA A FACTURA"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 10
        '        
        '    Case 11
        '        reporte.ReportParameters("txtDocumento").Value = "Remisión No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
        '    Case 12
        '        reporte.ReportParameters("txtDocumento").Value = "Nota de Salida No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: VERIFICAR CANT. DE PRODUCTOS QUE SALEN DEL ALMACÉN"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 13
        '        reporte.ReportParameters("txtDocumento").Value = "Póliza No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ANEXAR PÓLIZA A FACTURA"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 16
        '        reporte.ReportParameters("txtDocumento").Value = "Req. de Mat. No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "Cotizar__ Solicitar al Proveedor __ Tiempo de Entrega______ Opción de Pago_______"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 17
        '        reporte.ReportParameters("txtDocumento").Value = "Nómina No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones1").Value = "RECIBÍ DE CONFORMIDAD EL SALARIO DEVENGADO POR LOS DAIS QUE LABORE EN EL PERIODO DE TIEMPO QUE DETERMINA ESTE"
        '        reporte.ReportParameters("txtObservaciones2").Value = "RECIBO, NO RESERVANDOME ACCIÓN QUE EJERCITAR NI PRESENTE  NI FUTURA EN CONTRA DE LA EMPRESA POR CONCEPTO"
        '        reporte.ReportParameters("txtObservaciones3").Value = "DE 4 SUELDOS, DIFERENCIAS DE SALARIO, HORAS EXTRAS, SÉPTIMOS DÍAS, DÍAS FESTIVOS NI DE NINGUNA NATURALEZA"
        '        reporte.ReportParameters("txtObservaciones4").Value = "PERCEPCIONES Y DEDUCCIONES DE TRABAJADOR"
        'End Select

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
        reporte.ReportParameters("txtObservaciones1").Value = instrucciones
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
        reporte.ReportParameters("txtNombreElabora").Value = Session("nombre").ToString
        reporte.ReportParameters("txtNombreAutoriza").Value = "Erick Pruneda"

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


    Public Function GeneraPDF_parte2(ByVal cfdid As Long) As Telerik.Reporting.Report
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
        Dim tipocontribuyenteid As Integer = 0

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

        Dim reporte As New formato_cotizacion_parte2
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("txtFechaEmision").Value = Now.ToShortDateString & TimeOfDay.ToString("h:mm:ss tt")



        reporte.ReportParameters("paramImgLogo1").Value = Server.MapPath("~/portalcfd/logos/udara.jpeg")
        reporte.ReportParameters("paramImgLogo2").Value = Server.MapPath("~/portalcfd/logos/logo_carroll.jpeg")
        reporte.ReportParameters("paramImgLogo3").Value = Server.MapPath("~/portalcfd/logos/logo_CarrollAir.jpeg")
        reporte.ReportParameters("paramImgLogo4").Value = Server.MapPath("~/portalcfd/logos/logo_fst.jpeg")
        reporte.ReportParameters("txtDocumento").Value = "Cotización No. " & serie.ToString & folio.ToString


        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
        'Select Case tipoid
        '    Case 7  '   Pedido
        '        reporte.ReportParameters("txtDocumento").Value = "Pedido No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: SEÑALAR FECHA DE ENTREGA_________________________________"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 8
        '        reporte.ReportParameters("txtDocumento").Value = "Pago No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: GUARDAR COMPROBANTE PARA CUALQUIER ACLARACIÓN"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 9
        '        reporte.ReportParameters("txtDocumento").Value = "Orden de Compra No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: FAVOR DE ANEXAR ORDEN DE COMPRA A FACTURA"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 10
        '        
        '    Case 11
        '        reporte.ReportParameters("txtDocumento").Value = "Remisión No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
        '    Case 12
        '        reporte.ReportParameters("txtDocumento").Value = "Nota de Salida No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: VERIFICAR CANT. DE PRODUCTOS QUE SALEN DEL ALMACÉN"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 13
        '        reporte.ReportParameters("txtDocumento").Value = "Póliza No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ANEXAR PÓLIZA A FACTURA"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 16
        '        reporte.ReportParameters("txtDocumento").Value = "Req. de Mat. No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "Cotizar__ Solicitar al Proveedor __ Tiempo de Entrega______ Opción de Pago_______"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 17
        '        reporte.ReportParameters("txtDocumento").Value = "Nómina No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones1").Value = "RECIBÍ DE CONFORMIDAD EL SALARIO DEVENGADO POR LOS DAIS QUE LABORE EN EL PERIODO DE TIEMPO QUE DETERMINA ESTE"
        '        reporte.ReportParameters("txtObservaciones2").Value = "RECIBO, NO RESERVANDOME ACCIÓN QUE EJERCITAR NI PRESENTE  NI FUTURA EN CONTRA DE LA EMPRESA POR CONCEPTO"
        '        reporte.ReportParameters("txtObservaciones3").Value = "DE 4 SUELDOS, DIFERENCIAS DE SALARIO, HORAS EXTRAS, SÉPTIMOS DÍAS, DÍAS FESTIVOS NI DE NINGUNA NATURALEZA"
        '        reporte.ReportParameters("txtObservaciones4").Value = "PERCEPCIONES Y DEDUCCIONES DE TRABAJADOR"
        'End Select

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
        reporte.ReportParameters("txtObservaciones1").Value = instrucciones
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
        reporte.ReportParameters("txtNombreElabora").Value = Session("nombre").ToString
        reporte.ReportParameters("txtNombreAutoriza").Value = "Erick Pruneda"

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



    Public Function GeneraPDF_images(ByVal cfdid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)


        Dim plantillaid As Integer = 1

        Dim ds As DataSet = New DataSet

        Try
            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                plantillaid = rs("plantillaid")

            End If
            rs.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try



        Dim reporte As New formato_cotizaciones_images
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid

        Return reporte

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
        Dim tipocontribuyenteid As Integer = 0

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

        'Dim reporte As New Formatos.formato_cotizaciones_osdan
        Dim reporte As New formato_cotizaciones_osdan
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("txtFechaEmision").Value = Now.ToShortDateString & TimeOfDay.ToString("h:mm:ss tt")

        reporte.ReportParameters("paramImgLogo1").Value = Server.MapPath("~/portalcfd/logos/udara.jpeg")
        reporte.ReportParameters("paramImgLogo2").Value = Server.MapPath("~/portalcfd/logos/logo_carroll.jpeg")
        reporte.ReportParameters("paramImgLogo3").Value = Server.MapPath("~/portalcfd/logos/logo_CarrollAir.jpeg")
        reporte.ReportParameters("paramImgLogo4").Value = Server.MapPath("~/portalcfd/logos/logo_fst.jpeg")

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
        reporte.ReportParameters("txtObservaciones1").Value = instrucciones
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
        reporte.ReportParameters("txtNombreElabora").Value = Session("nombre").ToString
        reporte.ReportParameters("txtNombreAutoriza").Value = "Erick Pruneda"

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

    Public Function GeneraPDF_parte3(ByVal cfdid As Long) As Telerik.Reporting.Report
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
        Dim tipocontribuyenteid As Integer = 0

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

        Dim reporte As New formato_cotizacion_parte3
        reporte.ReportParameters("plantillaId").Value = plantillaid
        reporte.ReportParameters("cfdiId").Value = cfdid
        reporte.ReportParameters("txtFechaEmision").Value = Now.ToShortDateString & TimeOfDay.ToString("h:mm:ss tt")




        reporte.ReportParameters("paramImgLogo1").Value = Server.MapPath("~/portalcfd/logos/udara.jpeg")
        reporte.ReportParameters("paramImgLogo2").Value = Server.MapPath("~/portalcfd/logos/logo_carroll.jpeg")
        reporte.ReportParameters("paramImgLogo3").Value = Server.MapPath("~/portalcfd/logos/logo_CarrollAir.jpeg")
        reporte.ReportParameters("paramImgLogo4").Value = Server.MapPath("~/portalcfd/logos/logo_fst.jpeg")
        reporte.ReportParameters("txtDocumento").Value = "Cotización No. " & serie.ToString & folio.ToString


        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
        'Select Case tipoid
        '    Case 7  '   Pedido
        '        reporte.ReportParameters("txtDocumento").Value = "Pedido No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: SEÑALAR FECHA DE ENTREGA_________________________________"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 8
        '        reporte.ReportParameters("txtDocumento").Value = "Pago No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: GUARDAR COMPROBANTE PARA CUALQUIER ACLARACIÓN"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 9
        '        reporte.ReportParameters("txtDocumento").Value = "Orden de Compra No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: FAVOR DE ANEXAR ORDEN DE COMPRA A FACTURA"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 10
        '        
        '    Case 11
        '        reporte.ReportParameters("txtDocumento").Value = "Remisión No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ESTE COMPROBANTE NO TIENE VALOR FISCAL"
        '    Case 12
        '        reporte.ReportParameters("txtDocumento").Value = "Nota de Salida No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: VERIFICAR CANT. DE PRODUCTOS QUE SALEN DEL ALMACÉN"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 13
        '        reporte.ReportParameters("txtDocumento").Value = "Póliza No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "NOTA: ANEXAR PÓLIZA A FACTURA"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 16
        '        reporte.ReportParameters("txtDocumento").Value = "Req. de Mat. No. " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones4").Value = "Cotizar__ Solicitar al Proveedor __ Tiempo de Entrega______ Opción de Pago_______"
        '        reporte.ReportParameters("txtVigencia").Value = "Vigencia: "
        '    Case 17
        '        reporte.ReportParameters("txtDocumento").Value = "Nómina No.    " & serie.ToString & folio.ToString
        '        reporte.ReportParameters("txtObservaciones1").Value = "RECIBÍ DE CONFORMIDAD EL SALARIO DEVENGADO POR LOS DAIS QUE LABORE EN EL PERIODO DE TIEMPO QUE DETERMINA ESTE"
        '        reporte.ReportParameters("txtObservaciones2").Value = "RECIBO, NO RESERVANDOME ACCIÓN QUE EJERCITAR NI PRESENTE  NI FUTURA EN CONTRA DE LA EMPRESA POR CONCEPTO"
        '        reporte.ReportParameters("txtObservaciones3").Value = "DE 4 SUELDOS, DIFERENCIAS DE SALARIO, HORAS EXTRAS, SÉPTIMOS DÍAS, DÍAS FESTIVOS NI DE NINGUNA NATURALEZA"
        '        reporte.ReportParameters("txtObservaciones4").Value = "PERCEPCIONES Y DEDUCCIONES DE TRABAJADOR"
        'End Select

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
        reporte.ReportParameters("txtObservaciones1").Value = instrucciones
        reporte.ReportParameters("txtImporte").Value = FormatCurrency(importe, 2).ToString
        reporte.ReportParameters("txtIVA").Value = FormatCurrency(iva, 2).ToString
        reporte.ReportParameters("txtSubtotal").Value = FormatCurrency(importe + iva, 2).ToString
        reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
        reporte.ReportParameters("txtTotal").Value = FormatCurrency((importe + iva), 2).ToString
        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
        reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
        reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
        reporte.ReportParameters("txtNombreElabora").Value = Session("nombre").ToString
        reporte.ReportParameters("txtNombreAutoriza").Value = "Erick Pruneda"

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
        '
        '
        '
        '
        'ConsultaCarpetaAlmacenXML()
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        ' leer del fichero e ignorar los nodos vacios
        FlujoReader = New XmlTextReader(Server.MapPath("cfd_storage") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        ' analizar el fichero y presentar cada nodo
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
                            ElseIf FlujoReader.Name = "noCertificadoSAT" Then
                                noCertificadoSAT = FlujoReader.Value
                            ElseIf FlujoReader.Name = "selloCFD" Then
                                selloCFD = FlujoReader.Value
                            ElseIf FlujoReader.Name = "version" Then
                                Version = FlujoReader.Value
                            End If
                        Next
                    End If
            End Select
        End While
        '
        '
        '
        '
        '
        '
        '
        '
        '
        '
        ''
        'Dim s_RutaRespuestaPAC As String = Server.MapPath("cfd_storage") & "\" & "link_timbre_" & serie.ToString & folio.ToString & ".xml"
        'Dim respuestaPAC As New Timbrado()
        'Dim objStreamReader As New StreamReader(s_RutaRespuestaPAC)
        'Dim Xml As New XmlSerializer(respuestaPAC.[GetType]())
        'respuestaPAC = DirectCast(Xml.Deserialize(objStreamReader), Timbrado)
        'objStreamReader.Close()

        ''
        ''Crear el objeto timbre para asignar los valores de la respuesta PAC
        'fechaTimbrado = respuestaPAC.Items(0).Informacion(0).Timbre(0).FechaTimbrado
        'noCertificadoSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).noCertificadoSAT.ToString
        'selloCFD = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloCFD.ToString
        'selloSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloSAT.ToString
        'UUID = respuestaPAC.Items(0).Informacion(0).Timbre(0).UUID.ToString
        'Version = respuestaPAC.Items(0).Informacion(0).Timbre(0).version.ToString
        '
        Dim cadena As String = ""
        cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        Return cadena
        '
    End Function

    Private Sub generacbb(ByVal serie As String, ByVal folio As Long)
        Dim cadena As String = ""
        Dim UUID As String = ""
        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""

        '
        '   Obtiene datos del cfdi para construir string del CBB
        '

        '
        rfcE = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("cfd_storage") & "\" & "link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
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

    Protected Sub CreateWindowScript(ByVal cfdid As String, ByVal pagina As String)
        Dim window1 As New RadWindow()
        window1.NavigateUrl = pagina.ToString & "?cfdid=" & cfdid.ToString & "&tipodocumentoid=" & tipoid.SelectedValue.ToString & "&clienteid=" & clienteid.SelectedValue.ToString & "&fhaini=" & fha_ini.SelectedDate.Value.ToShortDateString & "&fhafin=" & fha_fin.SelectedDate.Value.ToShortDateString
        window1.VisibleOnPageLoad = True
        window1.Width = 950
        window1.Height = 700
        window1.Modal = True
        window1.Behaviors = WindowBehaviors.Close
        window1.Visible = True
        window1.VisibleOnPageLoad = True
        window1.AutoSize = False
        window1.VisibleStatusbar = True
        Me.Form.Controls.Add(window1)
    End Sub

    Private Sub DatosEmail(ByVal cfdid As Long)
        '
        tempcfdid.Value = cfdid
        '
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim tipoid As Integer = 0
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", connF)
        Try

            connF.Open()

            Dim rsF = cmdF.ExecuteReader

            If rsF.Read Then
                serie = rsF("serie").ToString
                folio = rsF("folio").ToString
                tipoid = rsF("tipoid")
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim correo As String = ""
        Dim email_from As String = ""
        Dim email_smtp_server As String = ""
        Dim email_smtp_username As String = ""
        Dim email_smtp_password As String = ""
        Dim email_smtp_port As String = ""

        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pEnviaEmail @cfdid='" & cfdid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '
            razonsocial = rs("razonsocial")
            correo = rs("email_to")
            email_from = rs("email_from")
            email_smtp_server = rs("email_smtp_server")
            email_smtp_username = rs("email_smtp_username")
            email_smtp_password = rs("email_smtp_password")
            email_smtp_port = rs("email_smtp_port")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        '
        If tipoid = 1 Then
            mensaje += "Estimado(a) Cliente, por este medio le hago llegar su Comprobante Fiscal Digital en formato PDF y en formato XML. Ud. puede imprimir el PDF e integrarlo a su contabilidad de forma tradicional o puede guardar el archivo XML que es fiscalmente válido."
        ElseIf tipoid = 10 Then
            mensaje += "Estimado(a) Cliente, por este medio le hago llegar la cotización solicitada en formato PDF. Ud. puede imprimir el PDF para su revisión. Gracias."
        End If
        '
        mensaje += "Atentamente." & vbCrLf & vbCrLf
        mensaje += razonsocial.ToString.ToUpper

        lblMensajeEmail.Text = ""
        txtFrom.Text = email_from.ToString
        txtTo.Text = correo.ToString
        txtSubject.Text = razonsocial & " - Comprobante Fiscal Digital"
        txtMenssage.Text = mensaje.ToString

        'RadWindow2.VisibleOnPageLoad = False
        RadWindow1.VisibleOnPageLoad = True

    End Sub

    Public Shared Function validarEmail(ByVal email As String) As Boolean
        Dim expresion As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        If Regex.IsMatch(email, expresion) Then
            If Regex.Replace(email, expresion, String.Empty).Length = 0 Then
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If

    End Function

    Private Sub VerAcuse(ByVal cfdi As Long)
        '
        '   Obtiene serie y folio y construye nombre del XML
        '
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim archivo_llave_privada As String = ""
        Dim contrasena_llave_privada As String = ""
        Dim archivoCertificado As String = ""
        Dim connX As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdX As New SqlCommand("select isnull(b.serie,'') as serie, isnull(b.folio,0) as folio from tblCFD b where b.id='" & cfdi.ToString & "'", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                serie = rs("serie")
                folio = rs("folio")
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        Finally
            connX.Close()
            connX.Dispose()
            connX = Nothing
        End Try
        '
        Dim UrlSAT As String = ""
        Dim FinalSelloDigitalEmisor As String = ""

        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""
        Dim UUID As String = ""
        Dim sello As String = ""
        '
        '   Obtiene datos del cfdi para construir string del CBB
        '
        rfcE = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "Total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
        sello = GetXmlAttribute(Server.MapPath("cfd_storage") & "\link_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloCFD", "tfd:TimbreFiscalDigital")
        FinalSelloDigitalEmisor = Mid(sello, (Len(sello) - 7))
        '
        Dim totalDec As Decimal = CType(total, Decimal)
        '
        UrlSAT = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & rfcE & "&rr=" & rfcR & "&tt=" & totalDec.ToString & "&fe=" & FinalSelloDigitalEmisor

        Dim script As String = "function f(){openRadWindowAcuse('" & UrlSAT & "'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

    End Sub

#End Region

#Region "Events"
    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        Dim ObjData As New DataControl
        cfdlist.DataSource = ObjData.FillDataSet("exec pCFD @cmd=22, @serie='" & txtSerie.Text & "', @folio='" & txtFolio.Text & "', @orden_compra='" & txtOrdenCompra.Text & "'")
        cfdlist.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call MuestraLista()
    End Sub
#End Region

#Region "Grid Filters"
    Private Sub SetGridFilters()
        cfdlist.GroupingSettings.CaseSensitive = False
        Dim Menu As GridFilterMenu = cfdlist.FilterMenu
        Dim item As RadMenuItem
        For Each item In Menu.Items
            'change the text for the StartsWith menu item
            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "EqualTo" Then
                item.Text = "Igual a"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If


        Next

    End Sub

    Private Sub cfdlist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles cfdlist.Init
        cfdlist.PagerStyle.NextPagesToolTip = "Ver mas"
        cfdlist.PagerStyle.NextPageToolTip = "Siguiente"
        cfdlist.PagerStyle.PrevPagesToolTip = "Ver mas"
        cfdlist.PagerStyle.PrevPageToolTip = "Atras"
        cfdlist.PagerStyle.LastPageToolTip = "Ultima Página"
        cfdlist.PagerStyle.FirstPageToolTip = "Primera Pagina"
        cfdlist.PagerStyle.PagerTextFormat = "{4}    Pagina {0} de {1}, Registros {2} al {3} de {5}"
        cfdlist.SortingSettings.SortToolTip = "Ordernar"
        cfdlist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        cfdlist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As GridFilterMenu = cfdlist.FilterMenu
        Dim i As Integer = 0
        While i < menu.Items.Count
            If menu.Items(i).Text = "NoFilter" Or _
               menu.Items(i).Text = "EqualTo" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If
        End While
    End Sub
#End Region

    Private Sub btnSendEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendEmail.Click
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim formatoBit As Boolean = False

        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCFD @cmd=18, @cfdid='" & tempcfdid.Value.ToString & "'", connF)
        Try

            connF.Open()

            Dim rsF = cmdF.ExecuteReader

            If rsF.Read Then
                serie = rsF("serie").ToString
                folio = rsF("folio").ToString
                formatoBit = CBool(rsF("formatoBit"))
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        '   Obtiene datos de la persona
        '
        Dim mensaje As String = ""
        Dim razonsocial As String = ""
        Dim correo As String = ""
        Dim email_from As String = ""
        Dim email_smtp_server As String = ""
        Dim email_smtp_username As String = ""
        Dim email_smtp_password As String = ""
        Dim email_smtp_port As String = ""

        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pEnviaEmail @cfdid='" & tempcfdid.Value.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            '       
            razonsocial = rs("razonsocial")
            correo = rs("email_to")
            email_from = rs("email_from")
            email_smtp_server = rs("email_smtp_server")
            email_smtp_username = rs("email_smtp_username")
            email_smtp_password = rs("email_smtp_password")
            email_smtp_port = rs("email_smtp_port")
            '
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing

        Dim delimit As Char() = New Char() {";"c, ","c}
        Dim novalidos As String = ""

        Dim objMM As New MailMessage

        For Each splitTo As String In txtCC.Text.Trim().Split(delimit)
            If validarEmail(splitTo.Trim()) Then
                objMM.CC.Add(splitTo.Trim())
            Else

                If novalidos = "" Then
                    novalidos += splitTo.Trim()
                Else
                    novalidos += "," & splitTo.Trim()
                End If

            End If
        Next


        If novalidos = "" Then
            objMM.To.Add(email_from)
            objMM.To.Add(txtTo.Text.Trim())
            objMM.From = New MailAddress(txtFrom.Text, "OSDAN - Atención a clientes")
            objMM.IsBodyHtml = False
            objMM.Priority = MailPriority.Normal
            objMM.Subject = txtSubject.Text
            objMM.Body = txtMenssage.Text
            '
            '   Agrega anexos
            '
            Dim AttachPDF As Net.Mail.Attachment
            Dim AttachXML As Net.Mail.Attachment

            If formatoBit Then
                AttachPDF = New Net.Mail.Attachment(Server.MapPath("~/portalcfd/pdf/") & "link_" & serie.ToString & folio.ToString & ".pdf")
                objMM.Attachments.Add(AttachPDF)
            Else
                AttachPDF = New Net.Mail.Attachment(Server.MapPath("~/portalcfd/pdf/") & "link_" & serie.ToString & folio.ToString & ".pdf")
                AttachXML = New Net.Mail.Attachment(Server.MapPath("~/portalcfd/cfd_storage/") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
                objMM.Attachments.Add(AttachPDF)
                objMM.Attachments.Add(AttachXML)
            End If

            Dim SmtpMail As New SmtpClient
            Try
                Dim SmtpUser As New Net.NetworkCredential
                SmtpUser.UserName = email_smtp_username
                SmtpUser.Password = email_smtp_password
                SmtpMail.EnableSsl = True
                SmtpMail.UseDefaultCredentials = False
                SmtpMail.Credentials = SmtpUser
                SmtpMail.Host = email_smtp_server
                SmtpMail.Port = email_smtp_port
                SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
                SmtpMail.Send(objMM)
                '
                '   Lo marca como enviado
                Dim ObjData As New DataControl
                ObjData.RunSQLQuery("exec pCFD @cmd=40, @cfdid='" & tempcfdid.Value.ToString & "'")
                ObjData = Nothing
                '
            Catch ex As Exception
                '
                '
            Finally
                SmtpMail = Nothing
            End Try
            objMM = Nothing
            '
            '
            Call MuestraLista()
            RadWindow1.VisibleOnPageLoad = False
            'RadWindow2.VisibleOnPageLoad = False
            '
            ''
        Else
            lblMensajeEmail.Text = "Formato de correo no válido: " & novalidos
        End If
    End Sub

    Protected Sub Facturar(ByVal remisionid As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=52, @remisionid='" & remisionid & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                If Convert.ToDecimal(rs("cfdid")) <= 0 Then
                    GetCFD(remisionid, rs("serieid"))
                Else
                    If rs("serieid") = 10 Then
                        Response.Redirect("~/portalcfd/Facturar.aspx?id=" & rs("cfdid").ToString & "&cid=" & remisionid.ToString, False)
                    ElseIf rs("serieid") = 11 Then
                        Response.Redirect("~/portalcfd/Facturar.aspx?id=" & rs("cfdid").ToString & "&rid=" & remisionid.ToString, False)
                    End If
                End If
            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            lblMensaje.Text = "Error: " + ex.Message.ToString()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Protected Sub GetCFD(ByVal remisionid As Integer, ByVal serieid As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=53, @remisionid='" & remisionid.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                If Convert.ToDecimal(rs("cfdid")) > 0 Then
                    If serieid = 10 Then
                        Response.Redirect("~/portalcfd/Facturar.aspx?id=" & rs("cfdid").ToString & "&cid=" & remisionid.ToString, False)
                    ElseIf serieid = 11 Then
                        Response.Redirect("~/portalcfd/Facturar.aspx?id=" & rs("cfdid").ToString & "&rid=" & remisionid.ToString, False)
                    End If
                End If
            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            lblMensaje.Text = "Error: " + ex.Message.ToString()
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

End Class