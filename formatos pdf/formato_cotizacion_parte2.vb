
Imports System.ComponentModel
    Imports System.Drawing
    Imports System.Windows.Forms
    Imports Telerik.Reporting
    Imports Telerik.Reporting.Drawing
    Imports System.Data.SqlClient
Partial Public Class formato_cotizacion_parte2
    Inherits Telerik.Reporting.Report
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub formato_cfdi_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource
        Dim cfdiId As Long = Me.ReportParameters("cfdiId").Value


        'Dim ta As New cfdiRowsTableAdapters.pCFDTableAdapter
        'ta.Connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString
        'Dim ds As cfdiRows.pCFDDataTable = ta.GetData(3, cfdiId)

        'Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        'processingReport.DataSource = ds


        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCFD @cmd=3, @cfdid='" & cfdiId.ToString & "'", conn)

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