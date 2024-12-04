Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class PepsicoAddenda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle
        '

        If Not IsPostBack Then
            Call GetData()
        End If

    End Sub

    Private Sub GetData()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pAddendas @cmd='2', @clienteId='1'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                RegistroID.Value = rs("id")
                AddendaID.Value = rs("addendaId")
                txtProvedorId.Text = rs("proveedorId")
                txtVersion.Text = rs("version")

                InsertOrUpdate.Value = 1

            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Protected Sub btnAgregarAddenda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarAddenda.Click

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            If InsertOrUpdate.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pAddendas @cmd=3, @proveedorId='" & txtProvedorId.Text & "', @version='" & txtVersion.Text & "'", conn)
                conn.Open()
                cmd.ExecuteReader()

                conn.Close()
                conn.Dispose()
                Call GetData()

                txtProvedorId.Text = ""
                txtVersion.Text = ""
                lblMensaje.Text = "Datos registrados"
            Else
                Dim cmd As New SqlCommand("EXEC pAddendas @cmd=4, @id='" & RegistroID.Value & "', @proveedorId='" & txtProvedorId.Text & "', @version='" & txtVersion.Text & "'", conn)
                conn.Open()
                cmd.ExecuteReader()

                conn.Close()
                conn.Dispose()
                lblMensaje.Text = "Datos actualizados"
            End If

            txtProvedorId.Text = ""
            txtVersion.Text = ""

        Catch ex As Exception
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

End Class