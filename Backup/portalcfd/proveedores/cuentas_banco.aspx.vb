Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Public Class cuentas_banco
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ObjData As New DataControl
            ObjData.Catalogo(bancoid, "select id,nombre from tblBanco order by nombre", 0)
            ObjData = Nothing
            Call muestraCuentas()
        End If
    End Sub

    Private Sub muestraCuentas()
        Dim ds As New DataSet()
        Dim ObjData As New DataControl
        ds = ObjData.FillDataSet("EXEC pCuentasBanco @cmd=1")
        grdCuentas.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdCuentas.DataSource = ds
        ObjData = Nothing
    End Sub

    Private Sub EditaCuenta(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pCuentasBanco @cmd=2, @cuentaid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                '
                panelRegistroCuenta.Visible = True
                '
                InsertOrUpdate.Value = 1
                bancoid.SelectedValue = rs("bancoid")
                txtCuenta.Text = rs("cuenta")
                cuentaID.Value = rs("id")
                '
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub EliminaCuenta(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pCuentasBanco @cmd=3, @cuentaid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelRegistroCuenta.Visible = False

            Response.Redirect("cuentas_banco.aspx", False)

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Protected Sub grdCuentas_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdCuentas.ItemCommand
        Select Case e.CommandName

            Case "cmdEdit"
                EditaCuenta(e.CommandArgument)

            Case "cmdDelete"
                EliminaCuenta(e.CommandArgument)

        End Select
    End Sub

    Protected Sub grdCuentas_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdCuentas.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Cuentas" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea borrar este cuenta de la base de datos?');")

            End If

        End If
    End Sub

    Protected Sub btnAgregaCuenta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregaCuenta.Click
        InsertOrUpdate.Value = 0
        txtCuenta.Text = ""
        bancoid.SelectedValue = 0
        panelRegistroCuenta.Visible = True
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim ObjData As New DataControl
        If InsertOrUpdate.Value = 0 Then
            ObjData.RunSQLQuery("EXEC pCuentasBanco @cmd=4, @bancoid='" & bancoid.SelectedValue.ToString & "', @cuenta='" & txtCuenta.Text & "'")
        Else
            ObjData.RunSQLQuery("EXEC pCuentasBanco @cmd=5, @bancoid='" & bancoid.SelectedValue.ToString & "', @cuenta='" & txtCuenta.Text & "', @cuentaid='" & cuentaID.Value.ToString & "'")
        End If

        ObjData = Nothing
        Response.Redirect("cuentas_banco.aspx", False)

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        InsertOrUpdate.Value = 0
        txtCuenta.Text = ""
        bancoid.SelectedValue = 0
        panelRegistroCuenta.Visible = False
    End Sub

End Class