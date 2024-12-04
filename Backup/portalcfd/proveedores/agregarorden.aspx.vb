Imports System.Data
Imports System.Threading
Imports System.Globalization
Imports System.Data.SqlClient

Partial Public Class agregarorden
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call CargaProveedores()
        End If
    End Sub

    Private Sub CargaProveedores()
        Dim ObjData As New DataControl
        ObjData.Catalogo(proveedorid, "select id, razonsocial as nombre from tblMisProveedores order by razonsocial", 0)
        'ObjData.Catalogo(tasaid, "select id, nombre from tblTasa order by id desc", 3)
        ObjData = Nothing
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/portalcfd/proveedores/ordenes_compra.aspx")
    End Sub

    Private Sub btnAddorder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddorder.Click
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ordenId As Long = 0
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("exec pOrdenCompra @cmd=2, @proveedorid='" & proveedorid.SelectedValue.ToString & "', @userid='" & Session("userid").ToString & "', @comentarios='" & txtComentarios.Text & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                ordenId = rs("ordenId")

            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()
            conn = Nothing

        End Try
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        Response.Redirect("~/portalcfd/proveedores/editarorden.aspx?id=" & ordenId.ToString)

    End Sub

End Class