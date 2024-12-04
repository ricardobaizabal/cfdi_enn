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
Partial Public Class Países
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Server.ScriptTimeout = 3600
        Me.Title = Resources.Resource.WindowsTitle
        If Not IsPostBack Then
        End If
    End Sub

#End Region

    Private Sub Paislist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles Paislist.NeedDataSource
        If Not e.IsFromDetailTable Then
            Paislist.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
            Paislist.DataSource = GetCatPaises()
        End If
    End Sub
    Function GetCatPaises() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogoSat @cmd=10", conn)

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
End Class