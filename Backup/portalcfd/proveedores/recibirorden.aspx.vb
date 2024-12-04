Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Globalization
Imports System.Threading

Partial Public Class recibirorden
    Inherits System.Web.UI.Page
    Private ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '
        If Not IsPostBack Then
            txtComentarios.Enabled = False
            Call MuestraDatosGenerales()
            Call CargaConceptos()
        End If
        '
    End Sub

    Private Sub MuestraDatosGenerales()
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
                'lblTasa.Text = rs("tasa").ToString
                txtComentarios.Text = rs("comentarios").ToString
                Session("estatusid") = rs("estatusid").ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub CargaConceptos()
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pOrdenCompra @cmd=10, @ordenid='" & Request("id").ToString & "'")
        conceptosList.DataSource = ds
        conceptosList.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
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
                '
                Dim btnAdd As ImageButton = CType(e.Item.FindControl("btnAdd"), ImageButton)
                btnAdd.Attributes.Add("onClick", "javascript:openRadWindow(" & e.Item.DataItem("id").ToString() & "); return false;")
                btnAdd.CausesValidation = False
                '
                If e.Item.DataItem("cantidad") = e.Item.DataItem("cantidad_recibida") Then
                    btnAdd.Visible = False
                End If
                '
            Case GridItemType.Footer
                If ds.Tables(0).Rows.Count > 0 Then
                    e.Item.Cells(4).Text = ds.Tables(0).Compute("sum(cantidad)", "")
                    e.Item.Cells(4).Font.Bold = True
                    e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
                    'e.Item.Cells(5).Text = ds.Tables(0).Compute("sum(cantidad_recibida)", "")
                    'e.Item.Cells(5).Font.Bold = True
                    'e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
                    e.Item.Cells(6).Text = FormatCurrency(ds.Tables(0).Compute("sum(total)", ""), 2).ToString
                    e.Item.Cells(6).Font.Bold = True
                    e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
                End If
        End Select
    End Sub

    Private Sub conceptosList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles conceptosList.NeedDataSource
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("exec pOrdenCompra @cmd=10, @ordenid='" & Request("id").ToString & "'")
        conceptosList.DataSource = ds
        ObjData = Nothing
    End Sub

    Private Sub EliminaConcepto(ByVal conceptoid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pOrdenCompra @cmd=6, @conceptoid='" & conceptoid.ToString & "'")
        ObjData = Nothing
    End Sub

End Class