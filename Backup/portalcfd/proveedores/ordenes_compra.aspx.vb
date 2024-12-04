Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO

Partial Public Class ordenes_compra
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fechaini.SelectedDate = Date.Now
            fechafin.SelectedDate = Date.Now

            Dim ObjData As New DataControl
            ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by razonsocial", 0, True)
            ObjData = Nothing

            If Not Session("proveedorid") Is Nothing Then
                proveedorid.SelectedValue = Session("proveedorid")
            End If

            If Not Session("fecini") Is Nothing Then
                fechaini.SelectedDate = Session("fecini")
            End If

            If Not Session("fecfin") Is Nothing Then
                fechafin.SelectedDate = Session("fecfin")
            End If

            If Not Session("no_orden") Is Nothing Then
                txtNoOrden.Text = Session("no_orden").ToString
            End If

            Call MuestraOrdenes()

        End If
    End Sub

    Private Sub btnAddOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOrder.Click
        Response.Redirect("~/portalcfd/proveedores/agregarorden.aspx")
    End Sub

    Private Sub MuestraOrdenes()
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Session("proveedorid") = proveedorid.SelectedValue
        Session("fecini") = CDate(fechaini.SelectedDate.Value.ToShortDateString)
        Session("fecfin") = CDate(fechafin.SelectedDate.Value.ToShortDateString)
        Session("no_orden") = txtNoOrden.Text
        '
        Dim ObjData As New DataControl
        Dim sql As String = ""
        If txtNoOrden.Text.ToString.Length > 0 Then
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "', @no_orden='" & Session("no_orden").ToString & "'"
        Else
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "'"
        End If
        '
        ds = ObjData.FillDataSet(sql)
        ordersList.DataSource = ds
        ordersList.DataBind()
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub ordersList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ordersList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/portalcfd/proveedores/editarorden.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdDelete"
                Call EliminaOrden(e.CommandArgument)
                Call MuestraOrdenes()
            Case "cmdReceive"
                Response.Redirect("~/portalcfd/proveedores/recibirorden.aspx?id=" & e.CommandArgument.ToString)
            Case "cmdSend"
                Try
                    Call EnviaOrdenProveedor(e.CommandArgument)
                    lblMensajeEnvioOrden.ForeColor = Drawing.Color.Green
                    lblMensajeEnvioOrden.Text = "Correo enviado al proveedor."
                Catch ex As Exception
                    lblMensajeEnvioOrden.ForeColor = Drawing.Color.Red
                    lblMensajeEnvioOrden.Text = "Error al enviar órden de compra al proveedor: " & ex.Message.ToString
                End Try
            Case "cmdDownload"
                DownloadPDF(e.CommandArgument)
        End Select
    End Sub

    Private Sub ordersList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles ordersList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                Dim btnSend As ImageButton = CType(e.Item.FindControl("btnSend"), ImageButton)
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('Va a borrar una orden de compra. ¿Desea continuar?');")

                If e.Item.DataItem("estatusid") = 4 Or e.Item.DataItem("estatusid") = 5 Then
                    btnDelete.Visible = False
                End If

                If e.Item.DataItem("estatusid") = 5 Then
                    btnSend.Visible = True
                Else
                    btnSend.Visible = False
                End If

        End Select
    End Sub

    Private Sub ordersList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ordersList.NeedDataSource
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Session("proveedorid") = proveedorid.SelectedValue
        Session("fecini") = CDate(fechaini.SelectedDate.Value.ToShortDateString)
        Session("fecfin") = CDate(fechafin.SelectedDate.Value.ToShortDateString)
        Session("no_orden") = txtNoOrden.Text
        '
        Dim ObjData As New DataControl
        Dim sql As String = ""
        If txtNoOrden.Text.ToString.Length > 0 Then
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "', @no_orden='" & Session("no_orden").ToString & "'"
        Else
            sql = "exec pOrdenCompra @cmd=1, @fechaini='" & fechaini.SelectedDate.Value.ToShortDateString & "', @fechafin='" & fechafin.SelectedDate.Value.ToShortDateString & "', @proveedorid='" & Session("proveedorid").ToString & "'"
        End If
        '
        ds = ObjData.FillDataSet(sql)
        ordersList.DataSource = ds
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
    End Sub

    Private Sub EliminaOrden(ByVal ordenid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=4, @ordenid='" & ordenid.ToString & "'")
        ObjData = Nothing
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Call MuestraOrdenes()
    End Sub

    Private Sub EnviaOrdenProveedor(ByVal ordenId As Integer)
        '
        Dim objData As New DataControl
        Dim dsPartidas As DataSet
        Dim comentarios As String = ""
        '
        Dim emailFrom As String = objData.RunSQLScalarQueryString("select isnull(email, 'epruneda.osdan@gmail.com')FROM tblUsuario WHERE id = " & Session("userid").ToString)
        'Dim emailTo As String = "gesquivel@linkium.mx"
        Dim emailTo As String = objData.RunSQLScalarQueryString("select isnull(p.email_contacto,'epruneda.osdan@gmail.com') as email from tblOrdenCompra o inner join tblMisProveedores p on p.id=o.proveedorid where o.id = '" & ordenId.ToString & "'")
        Dim total As Decimal = 0

        Dim BodyTxt As String = "<html><head></head><body style='font-family:arial; font-size:12px;'>"
        BodyTxt += "<br /><br />Estimado proveedor, se anexa la presente orden de compra.<br /><br />"
        BodyTxt += "<fieldset style='padding:10px;'>"

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pOrdenCompra @cmd=3, @ordenId='" & ordenId.ToString & "'", conn)
        Try
            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            If rs.Read Then
                BodyTxt += "<strong>Orden No.:</strong> " & ordenId.ToString & "<br />"
                BodyTxt += "<strong>Fecha:</strong> " & rs("fecha") & "<br />"
                BodyTxt += "<strong>Proveedor:</strong> " & rs("proveedor") & "<br />"
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        BodyTxt += "</fieldset><br />"
        BodyTxt += "<table border='1' cellpadding='3' cellspacing='0' style='width:1100px; border-color:#DBDFE4; font-family:arial; font-size:12px;'>"
        BodyTxt += "<tr><td style='background-color:#DBDFE4;font-weight: bold;'>Código</td><td style='background-color:#DBDFE4;font-weight: bold;'>Cantidad</td><td style='background-color:#DBDFE4;font-weight: bold;'>U. Medida</td><td style='background-color:#DBDFE4;font-weight: bold;'>Descripción</td><td style='background-color:#DBDFE4;font-weight: bold;'>Costo</td><td style='background-color:#DBDFE4;font-weight: bold;'>Total</td></tr>"

        Try

            dsPartidas = objData.FillDataSet("exec pOrdenCompra @cmd=7, @ordenId='" & ordenId.ToString & "'")
            If Not dsPartidas Is Nothing Then
                For Each row As DataRow In dsPartidas.Tables(0).Rows
                    BodyTxt += "<tr><td>" & _
                                        row("codigo").ToString & "</td><td>" & _
                                        row("cantidad").ToString & "</td><td>" & _
                                        row("unidad").ToString & "</td><td>" & _
                                        row("descripcion").ToString & "</td><td align='right'>" & _
                                        FormatCurrency(row("costo_estandar"), 2).ToString & "</td><td align='right'>" & _
                                        FormatCurrency(row("subtotal"), 2).ToString & "</td></tr>"
                    total = total + Convert.ToDecimal(row("subtotal"))
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        End Try

        BodyTxt += "<tr><td colspan='5' align='right'><strong>Total:</strong></td>"
        BodyTxt += "<td align='right'><strong>" & FormatCurrency(total, 2).ToString & "</strong>"
        BodyTxt += "</td></tr>"
        BodyTxt += "</table><br /><br /><br /><br />"
        BodyTxt += "<br /><br />OSDAN Productos y Soluciones de limpieza</body></html>"

        Dim FilePath = Server.MapPath("~/portalcfd/proveedores/oc/OrdenesCompra_") & ordenId.ToString & ".pdf"

        If Not File.Exists(FilePath) Then
            GuardaPDF(GeneraPDF(ordenId), FilePath)
        End If

        Dim AttachPDF As Net.Mail.Attachment
        AttachPDF = New Net.Mail.Attachment(FilePath)

        Dim objMM As New MailMessage
        objMM.Subject = "OSDAN - Órden de Compra No. " & ordenId.ToString
        objMM.To.Add(emailTo)
        objMM.From = New MailAddress(emailFrom, "OSDAN")
        objMM.IsBodyHtml = False
        objMM.Priority = MailPriority.Normal
        objMM.Body = BodyTxt
        objMM.IsBodyHtml = True
        objMM.Attachments.Add(AttachPDF)

        Dim SmtpMail As New SmtpClient
        Try
            Dim SmtpUser As New Net.NetworkCredential
            SmtpUser.UserName = "enviosweb@linkium.mx"
            SmtpUser.Password = "Link2020"
            SmtpUser.Domain = "smtp.linkium.mx"
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = SmtpUser
            SmtpMail.Host = "smtp.linkium.mx"
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpMail.Send(objMM)
        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
        Finally
            SmtpMail = Nothing
        End Try
        objMM = Nothing

        'Dim ObjEmail As New ObjComms
        'ObjEmail.EmailSubject = "OSDAN - Órden de Compra No. " & ordenId.ToString
        'ObjEmail.EmailBody = BodyTxt
        'ObjEmail.EmailTo = emailTo
        'ObjEmail.EmailCc = emailFrom
        'ObjEmail.EmailFrom = emailFrom
        'ObjEmail.EmailSend()
        'ObjEmail = Nothing

    End Sub

    Private Function GeneraPDF(ByVal IdOrden As Long) As Telerik.Reporting.Report
        Dim reporte As New FormatoOrdenCompra
        reporte.ReportParameters("pIdOrdenCompra").Value = IdOrden
        Return reporte
    End Function

    Private Sub DownloadPDF(ByVal IdOrdenCompra As Long)
        Try
            Dim FilePath = Server.MapPath("~/portalcfd/proveedores/oc/OrdenesCompra_") & IdOrdenCompra.ToString & ".pdf"
            GuardaPDF(GeneraPDF(IdOrdenCompra), FilePath)
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

End Class