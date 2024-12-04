Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class formato_complemento40
    Inherits Telerik.Reporting.Report
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub formato_cfdi_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource
        Dim cfdiId As Long = Me.ReportParameters("cfdiId").Value
        Dim strConexion As String = System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim conn As New SqlConnection(strConexion)
        Dim cmd As New SqlDataAdapter("EXEC pCFD @cmd=41, @cfdid='" & cfdiId.ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            '
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        processingReport.DataSource = ds
    End Sub


End Class