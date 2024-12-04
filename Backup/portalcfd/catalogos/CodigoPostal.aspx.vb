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
Partial Public Class CodigoPostal
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Server.ScriptTimeout = 3600
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
            Dim ObjData As New DataControl
            ObjData.Catalogo(estadoid, "select clave, nombre from tblEstado order by nombre", 19)
            ObjData = Nothing
        End If
    End Sub
#End Region

    Private Sub CodigoPostallist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles CodigoPostallist.NeedDataSource
        If Not e.IsFromDetailTable Then
            CodigoPostallist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            CodigoPostallist.DataSource = GetCatCodigoPostal()
        End If
    End Sub

    Function GetCatCodigoPostal() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogoSat @cmd=4, @clave='" & estadoid.SelectedValue & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CodigoPostallist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        CodigoPostallist.DataSource = GetCatCodigoPostal()
        CodigoPostallist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        estadoid.SelectedValue = 0
        CodigoPostallist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        CodigoPostallist.DataSource = GetCatCodigoPostal()
        CodigoPostallist.DataBind()
    End Sub

End Class