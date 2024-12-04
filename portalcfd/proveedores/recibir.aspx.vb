Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading

Partial Public Class recibir
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call MuestraDetallesPartida()
            Call CargaPartidasRecepcion()
        End If
    End Sub

    Private Sub MuestraDetallesPartida()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("exec pOrdenCompra @cmd=11, @conceptoid='" & Request("id").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                lblCodigo.Text = rs("ordenid")
                lblDescripcion.Text = rs("descripcion")
                lblCantidad.Text = rs("cantidad")
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try

            Dim cmd As New SqlCommand("exec pControlInventario @cmd=1, @ordendetalleid='" & Request("id").ToString & "', @costo_estandar='" & txtCostoEstandar.Text & "', @userid='" & Session("userid").ToString & "', @cantidad='" & txtCantidad.Text & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                lblMensaje.Text = rs("mensaje")
                Session("estatusid") = rs("estatusid")

                If Session("estatusid") = 5 Then
                    ScriptManager.RegisterStartupScript(Me, [GetType](), "close", "CloseModal();", True)
                End If

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        Call CargaPartidasRecepcion()
        '
    End Sub

    Private Sub CargaPartidasRecepcion()
        Dim ObjData As New DataControl
        conceptosList.DataSource = ObjData.FillDataSet("exec pControlInventario @cmd=2, @ordendetalleid='" & Request("id").ToString & "'")
        conceptosList.DataBind()
        ObjData = Nothing
    End Sub

    Private Sub EliminaPartida(ByVal partidaid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pControlInventario @cmd=3, @inventarioid='" & partidaid.ToString & "'")
        ObjData = Nothing
    End Sub

End Class