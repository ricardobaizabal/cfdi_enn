Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing
Imports System.Data.SqlClient

Partial Public Class formato_cotizaciones_osdan
    Inherits Telerik.Reporting.Report
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub formato_cfdi_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource
        'Dim cfdiId As Long = Me.ReportParameters("cfdiId").Value


        'Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        'Dim cmd As New SqlDataAdapter("EXEC pCFD @cmd=3, @cfdid='" & cfdiId.ToString & "'", conn)

        'Dim ds As DataSet = New DataSet

        'Try

        '    conn.Open()

        '    cmd.Fill(ds)

        '    conn.Close()
        '    conn.Dispose()

        'Catch ex As Exception
        '    '
        'Finally

        '    conn.Close()
        '    conn.Dispose()

        'End Try

        ' Crear DataTable
        Dim dataTable As New DataTable("TelerikDataSource")

        ' Agregar columnas
        dataTable.Columns.Add("Codigo", GetType(String))
        dataTable.Columns.Add("UrlImagen", GetType(String))
        dataTable.Columns.Add("Link", GetType(String))

        ' Generar filas aleatorias
        For i As Integer = 1 To 20
            Dim codigo As String = $"CODE-{i:000}"
            Dim urlImagen As String = $"./img.png"
            Dim link As String = $"https://www.google.com.mx/?hl=es-419"
            dataTable.Rows.Add(codigo, urlImagen, link)
        Next

        Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        processingReport.DataSource = dataTable

    End Sub

End Class