Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading
Imports System.IO

Partial Public Class editarorden
    Inherits System.Web.UI.Page
    Private ds As DataSet
    Private dsRecibidos As DataSet
    'Private validaAutorizacion As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call MuestraDatosGenerales()
            Call CargaConceptos()
            Call CargaConceptosRecibidos()
        End If
    End Sub

    Private Sub MuestraDatosGenerales()

        'Dim ObjData As New DataControl
        'validaAutorizacion = ObjData.RunSQLScalarQuery("exec pUsuarios @cmd=10, @userid='" & Session("userid").ToString & "'")
        'ObjData = Nothing

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("exec pOrdenCompra @cmd=3, @ordenid='" & Request("id").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                lblOrden.Text = rs("id").ToString
                lblEstatus.Text = rs("estatus").ToString
                lblProveedor.Text = rs("proveedor").ToString
                txtComentarios.Text = rs("comentarios").ToString
                Session("estatusid") = rs("estatusid").ToString
                '
                If Session("estatusid") = 5 Then
                    conceptosRecibidosList.Visible = True
                    conceptosList.Visible = False
                Else
                    conceptosRecibidosList.Visible = False
                    conceptosList.Visible = True
                End If

                '   Estatus
                '1  Abierta
                '2  En Proceso
                '3  Rechazada
                '4  Autorizada
                '5  Recibida
                '6  Cancelada

                'If rs("estatusid") = 1 Then
                '    btnAutorizar.Enabled = False
                '    btnAutorizar.ToolTip = "Operación no permitida."
                '    btnRechazar.Enabled = False
                '    btnRechazar.ToolTip = "Operación no permitida."
                '    btnFinalizar.Enabled = False
                '    btnFinalizar.ToolTip = "Operación no permitida."
                'ElseIf rs("estatusid") = 2 Then
                '    btnProcess.Enabled = False
                '    btnProcess.ToolTip = "Operación no permitida."
                '    btnAddorder.Enabled = False
                '    btnAddorder.ToolTip = "Operación no permitida."
                '    txtComentarios.Enabled = False
                '    txtSearch.Enabled = False
                '    btnAutorizar.Enabled = False
                '    btnAutorizar.ToolTip = "Operación no permitida."
                '    btnRechazar.Enabled = False
                '    btnRechazar.ToolTip = "Operación no permitida."
                '    btnFinalizar.Enabled = False
                '    btnFinalizar.ToolTip = "Operación no permitida."
                'ElseIf rs("estatusid") = 3 Then
                '    btnAddorder.Enabled = False
                '    btnAddorder.ToolTip = "Operación no permitida."
                '    btnProcess.Enabled = False
                '    btnProcess.ToolTip = "Operación no permitida."
                '    txtSearch.Enabled = False
                '    btnAutorizar.Enabled = False
                '    btnAutorizar.ToolTip = "Operación no permitida."
                '    btnRechazar.Enabled = False
                '    btnRechazar.ToolTip = "Operación no permitida."
                '    txtComentarios.Enabled = False
                '    btnFinalizar.Enabled = False
                '    btnFinalizar.ToolTip = "Operación no permitida."
                'ElseIf rs("estatusid") = 4 Then
                '    btnAddorder.Enabled = False
                '    btnAddorder.ToolTip = "Operación no permitida."
                '    btnProcess.Enabled = False
                '    btnProcess.ToolTip = "Operación no permitida."
                '    txtSearch.Enabled = False
                '    btnAutorizar.ToolTip = "Operación no permitida."
                '    btnRechazar.Enabled = False
                '    btnRechazar.ToolTip = "Operación no permitida."
                '    txtComentarios.Enabled = False
                '    btnAutorizar.Enabled = False
                '    btnProcessCancel.Enabled = False
                '    btnProcessCancel.ToolTip = "Operación no permitida."
                '    btnFinalizar.Enabled = True
                '    btnFinalizar.ToolTip = ""
                '    'If Session("perfilid") = 1 Or validaAutorizacion = True Then
                '    '    btnProcessCancel.Enabled = True
                '    '    btnProcessCancel.ToolTip = ""
                '    'End If
                'ElseIf rs("estatusid") = 5 Then
                '    btnAddorder.Enabled = False
                '    btnAddorder.ToolTip = "Operación no permitida."
                '    btnProcess.Enabled = False
                '    btnProcess.ToolTip = "Operación no permitida."
                '    txtSearch.Enabled = False
                '    btnAutorizar.ToolTip = "Operación no permitida."
                '    btnRechazar.Enabled = False
                '    btnRechazar.ToolTip = "Operación no permitida."
                '    txtComentarios.Enabled = False
                '    btnAutorizar.Enabled = False
                '    btnProcessCancel.Enabled = False
                '    btnProcessCancel.ToolTip = "Operación no permitida."
                '    btnFinalizar.Enabled = False
                '    btnFinalizar.ToolTip = "Operación no permitida."
                'ElseIf rs("estatusid") = 6 Then
                '    btnAddorder.Enabled = False
                '    btnAddorder.ToolTip = "Operación no permitida."
                '    btnProcess.Enabled = False
                '    btnProcess.ToolTip = "Operación no permitida."
                '    txtSearch.Enabled = False
                '    btnAutorizar.ToolTip = "Operación no permitida."
                '    btnRechazar.Enabled = False
                '    btnRechazar.ToolTip = "Operación no permitida."
                '    txtComentarios.Enabled = False
                '    btnAutorizar.Enabled = False
                '    btnProcessCancel.Enabled = False
                '    btnProcessCancel.ToolTip = "Operación no permitida."
                '    btnFinalizar.Enabled = False
                '    btnFinalizar.ToolTip = "Operación no permitida."
                'End If

                'If rs("estatusid") = 2 Then
                '    btnProcess.Enabled = False
                '    btnProcess.ToolTip = "Operación no permitida."
                '    txtComentarios.Enabled = False
                '    btnAddorder.Enabled = True
                '    btnAddorder.ToolTip = ""
                '    txtSearch.Enabled = True
                '    btnProcessCancel.Enabled = True
                '    btnProcessCancel.ToolTip = ""
                '    btnAutorizar.Enabled = True
                '    btnAutorizar.ToolTip = ""
                '    btnRechazar.Enabled = True
                '    btnRechazar.ToolTip = ""
                'End If

                If rs("estatusid") = 5 Or rs("estatusid") = 6 Then
                    btnAddorder.Enabled = False
                    btnAddorder.ToolTip = "Operación no permitida."

                    btnFinalizar.Enabled = False
                    btnFinalizar.ToolTip = "Operación no permitida."

                    btnProcessCancel.Enabled = False
                    btnProcessCancel.ToolTip = "Operación no permitida."

                    txtComentarios.Enabled = False
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub CargaConceptos()
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pOrdenCompra @cmd=7, @ordenid='" & Request("id").ToString & "'")
        conceptosList.DataSource = ds
        conceptosList.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        panelSearch.Visible = True
        btnAgregaConceptos.Visible = True
        Dim ObjData As New DataControl
        resultslist.DataSource = ObjData.FillDataSet("exec pMisProductos @cmd=6, @txtSearch='" & txtSearch.Text & "'")
        resultslist.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub resultslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles resultslist.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                Call AgregaItem(e.CommandArgument, e.Item)
        End Select
    End Sub

    Private Sub resultslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles resultslist.NeedDataSource
        Dim ObjData As New DataControl
        resultslist.DataSource = ObjData.FillDataSet("exec pMisProductos @cmd=1, @txtSearch='" & txtSearch.Text & "'")
        ObjData = Nothing
    End Sub

    Private Sub AgregaItem(ByVal productoId As Long, ByVal item As GridItem)
        Dim txtCantidad As RadNumericTextBox = DirectCast(item.FindControl("txtCantidad"), RadNumericTextBox)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=5, @ordenId='" & Request("id").ToString & "', @cantidad='" & txtCantidad.Text & "', @productoId='" & productoId.ToString & "'")
        ObjData = Nothing
        '
        txtSearch.Text = ""
        panelSearch.Visible = False
        Call CargaConceptos()
        '
    End Sub

    Private Sub conceptosList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles conceptosList.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                Call EliminaConcepto(e.CommandArgument)
                Call CargaConceptos()
        End Select
    End Sub

    Private Sub conceptosList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles conceptosList.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim txtCantidad As RadNumericTextBox = CType(e.Item.FindControl("txtCantidad"), RadNumericTextBox)
                txtCantidad.Text = e.Item.DataItem("cantidad")
                '
                Dim btnDelete As ImageButton = CType(e.Item.FindControl("btnDelete"), ImageButton)
                '
                If Session("estatusid") <> 1 Then
                    btnDelete.Enabled = False
                    btnDelete.ToolTip = "Operación no permitida."
                Else
                    btnDelete.Attributes.Add("onclick", "javascript:return confirm('Va a eliminar un concepto de la orden de compra. ¿Desea continuar?');")
                End If
                '
                'If Session("estatusid") = 2 And validaAutorizacion = True Then
                '    btnDelete.Attributes.Add("onclick", "javascript:return confirm('Va a eliminar un concepto de la orden de compra. ¿Desea continuar?');")
                '    btnDelete.Enabled = True
                '    btnDelete.ToolTip = ""
                'End If
                '
                If Session("estatusid") = 3 Or Session("estatusid") = 5 Or Session("estatusid") = 6 Then
                    txtCantidad.Enabled = False
                End If
                '
            Case GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(5).Text = ds.Tables(0).Compute("sum(cantidad)", "")
                    e.Item.Cells(5).Font.Bold = True
                    e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Center
                    '
                    e.Item.Cells(7).Text = FormatCurrency(ds.Tables(0).Compute("sum(subtotal)", ""), 2).ToString
                    e.Item.Cells(7).Font.Bold = True
                    e.Item.Cells(7).HorizontalAlign = HorizontalAlign.Right
                    '
                    e.Item.Cells(8).Text = FormatCurrency(ds.Tables(0).Compute("sum(impuesto)", ""), 2).ToString
                    e.Item.Cells(8).Font.Bold = True
                    e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                    '
                    e.Item.Cells(9).Text = FormatCurrency(ds.Tables(0).Compute("sum(total)", ""), 2).ToString
                    e.Item.Cells(9).Font.Bold = True
                    e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                End If
        End Select
    End Sub

    Private Sub conceptosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles conceptosList.NeedDataSource
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pOrdenCompra @cmd=7, @ordenid='" & Request("id").ToString & "'")
        conceptosList.DataSource = ds
        ObjData = Nothing
    End Sub

    Private Sub EliminaConcepto(ByVal conceptoid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=6, @conceptoid='" & conceptoid.ToString & "'")
        ObjData = Nothing
    End Sub

    Protected Sub btnAddorder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddorder.Click
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim conceptoId As Long = 0
        Dim ObjData As New DataControl
        For Each row As GridDataItem In conceptosList.MasterTableView.Items
            conceptoId = row.GetDataKeyValue("id")
            Dim txtCantidad As RadNumericTextBox = DirectCast(row.FindControl("txtCantidad"), RadNumericTextBox)
            ObjData.RunSQLQuery("exec pOrdenCompra @cmd=8, @conceptoid='" & conceptoId.ToString & "', @cantidad='" & txtCantidad.Text & "'")
        Next
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        lblMensaje.Text = "Datos actualizados."
        '
        Call CargaConceptos()
        '
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        txtSearch.Text = ""
        panelSearch.Visible = False
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=9, @ordenid='" & Request("id").ToString & "'")
        ObjData = Nothing
        'Call EnviaOrdenParaAutorizacion()
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
    End Sub

    Private Sub EnviaOrdenParaAutorizacion()
        '
        Dim objData As New DataControl
        Dim dsPartidas As DataSet
        Dim dsSubtotales As DataSet
        Dim comentarios As String = ""
        '
        Dim emailFrom As String = objData.RunSQLScalarQueryString("select isnull(email, 'ventas@comercialtrevisa.com')  FROM tblUsuario WHERE id = " & Session("userid").ToString)
        Dim emailTo As String = ""
        Dim token As String = ""

        Dim conexion As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim comando As New SqlCommand("select top 1 isnull(email, 'david.trevinoespejo@gmail.com') as email, convert(varchar(100),token) as token from tblUsuario where isnull(autorizaOrdenBit,0)=1", conexion)

            conexion.Open()

            Dim rs As SqlDataReader
            rs = comando.ExecuteReader()

            If rs.Read Then
                emailTo = rs("email").ToString
                token = rs("token").ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conexion.Close()
            conexion.Dispose()
            conexion = Nothing
        End Try

        'emailTo = "gesquivel@linkium.mx"

        Dim total As Decimal = 0

        Dim BodyTxt As String = "<html><head></head><body style='font-family:arial; font-size:12px;'>"
        BodyTxt += "<br /><br />Se anexa la órden de compra para su autorización: <br /><br />"
        BodyTxt += "<fieldset style='padding:10px;'>"

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pOrdenCompra @cmd=3, @ordenId='" & Request("id").ToString & "'", conn)
        Try
            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            If rs.Read Then
                BodyTxt += "<strong>Orden No.:</strong> " & Request("id").ToString & "<br />"
                BodyTxt += "<strong>Proveedor:</strong> " & rs("proveedor") & "<br />"
                BodyTxt += "<strong>Comentarios:</strong> " & rs("comentarios") & "<br />"
                comentarios = rs("comentarios")
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try

        BodyTxt += "</fieldset><br />"
        BodyTxt += "<table border='1' cellpadding='3' cellspacing='0' style='width:1100px; border-color:#DBDFE4; font-family:arial; font-size:12px;'>"
        BodyTxt += "<tr><td style='background-color:#DBDFE4;'>Código</td><td style='background-color:#DBDFE4;'>Código Barras</td><td style='background-color:#DBDFE4;'>Descripción</td><td style='background-color:#DBDFE4;'>U. Medida</td><td style='background-color:#DBDFE4;'>Cantidad</td><td style='background-color:#DBDFE4;'>Costo</td><td style='background-color:#DBDFE4;'>Total</td></tr>"

        Try

            dsPartidas = objData.FillDataSet("exec pOrdenCompra @cmd=7, @ordenId='" & Request("id").ToString & "'")
            If Not dsPartidas Is Nothing Then
                For Each row As DataRow In dsPartidas.Tables(0).Rows
                    BodyTxt += "<tr><td>" & _
                                        row("codigo").ToString & "</td><td>" & _
                                        row("codigo_barras").ToString & "</td><td>" & _
                                        row("descripcion").ToString & "</td><td>" & _
                                        row("unidad").ToString & "</td><td align='right'>" & _
                                        row("cantidad").ToString & "</td><td align='right'>" & _
                                        FormatCurrency(row("costo_estandar"), 2).ToString & "</td><td align='right'>" & _
                                        FormatCurrency(row("subtotal"), 2).ToString & "</td></tr>"
                    total = total + Convert.ToDecimal(row("subtotal"))
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

        BodyTxt += "<tr><td colspan='6' align='right'><strong>Total:</strong></td>"
        BodyTxt += "<td align='right'><strong>" & FormatCurrency(total, 2).ToString & "</strong>"
        BodyTxt += "</td></tr>"

        BodyTxt += "</table><br /><br /><br /><br />"

        BodyTxt += "<strong>Comentarios:</strong> " & comentarios.ToString & "<br /><br /><br />"
        BodyTxt += "<a href='http://" & HttpContext.Current.Request.Url.Host & "/Default.aspx?token=" & token.ToString & "&id=" & Request("id").ToString & "'>VER DETALLE</a>" & "<br /><br />"
        BodyTxt += "<br /><br />Comercial Trevisa S.A. de C.V. ©   Derechos reservados 2015</body></html>"

        Dim ObjEmail As New ObjComms
        ObjEmail.EmailSubject = "Comercial Trevisa - Autorización de Orden de Compra No. " & Request("id").ToString
        ObjEmail.EmailBody = BodyTxt
        ObjEmail.EmailTo = emailTo
        ObjEmail.EmailCc = emailFrom
        ObjEmail.EmailFrom = emailFrom
        ObjEmail.EmailSend()
        ObjEmail = Nothing
        '
    End Sub

    Private Sub btnAgregaConceptos_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregaConceptos.Click
        Dim productoId As Long = 0
        Dim factor As Decimal = 0
        Dim ObjData As New DataControl
        For Each row As GridDataItem In resultslist.MasterTableView.Items
            productoId = row.GetDataKeyValue("productoid")
            Dim txtCantidad As RadNumericTextBox = DirectCast(row.FindControl("txtCantidad"), RadNumericTextBox)

            If Convert.ToDecimal(txtCantidad.Text.ToString) > 0 Then
                ObjData.RunSQLQuery("exec pOrdenCompra @cmd=5, @ordenId='" & Request("id").ToString & "', @cantidad='" & txtCantidad.Text & "', @productoId='" & productoId.ToString & "'")
            End If
        Next
        ObjData = Nothing
        txtSearch.Text = ""
        panelSearch.Visible = False
        btnAgregaConceptos.Visible = False
        Call CargaConceptos()
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Call EnviaOrdenParaAutorizacion()
    End Sub

    Private Sub btnAutorizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAutorizar.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=14, @ordenid='" & Request("id").ToString & "'")
        ObjData = Nothing
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
    End Sub

    Private Sub btnRechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRechazar.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=13, @ordenid='" & Request("id").ToString & "'")
        ObjData = Nothing
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
    End Sub

    Private Sub btnProcessCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProcessCancel.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=12, @ordenid='" & Request("id").ToString & "'")
        ObjData = Nothing
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=16, @ordenid='" & Request("id").ToString & "'")
        ObjData = Nothing
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
    End Sub

    Private Sub CargaConceptosRecibidos()
        Dim ObjData As New DataControl
        dsRecibidos = ObjData.FillDataSet("exec pOrdenCompra @cmd=10, @ordenid='" & Request("id").ToString & "'")
        conceptosRecibidosList.DataSource = dsRecibidos
        conceptosRecibidosList.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub conceptosRecibidosList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles conceptosRecibidosList.ItemDataBound
        Select Case e.Item.ItemType
            'Case GridItemType.Item, GridItemType.AlternatingItem
            Case GridItemType.Footer
                If dsRecibidos.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(4).Text = dsRecibidos.Tables(0).Compute("sum(cantidad)", "")
                    e.Item.Cells(4).Font.Bold = True
                    e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Center
                    e.Item.Cells(5).Text = dsRecibidos.Tables(0).Compute("sum(cantidad_recibida)", "")
                    e.Item.Cells(5).Font.Bold = True
                    e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Center
                    e.Item.Cells(7).Text = FormatCurrency(dsRecibidos.Tables(0).Compute("sum(total)", ""), 2).ToString
                    e.Item.Cells(7).Font.Bold = True
                    e.Item.Cells(7).HorizontalAlign = HorizontalAlign.Right
                End If
        End Select
    End Sub

    Private Sub conceptosRecibidosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles conceptosRecibidosList.NeedDataSource
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pOrdenCompra @cmd=10, @ordenid='" & Request("id").ToString & "'")
        conceptosRecibidosList.DataSource = dsRecibidos
        ObjData = Nothing
    End Sub

End Class