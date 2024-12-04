Partial Public Class formato_cotizacion_parte1
    Inherits Telerik.Reporting.Report
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub formato_cotizaciones_images_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource
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
        dataTable.Columns.Add("Dia", GetType(String))
        dataTable.Columns.Add("Mes", GetType(String))
        dataTable.Columns.Add("Year", GetType(String))
        dataTable.Columns.Add("Idcotizacion", GetType(String))

        Dim codigo As String = $"CODs"
        Dim dia As String = $"27"
        Dim mes As String = $"Diciembre"
        Dim year As String = $"2024"
        Dim idcotizacion As String = $"15245"
        Dim urlImagen As String = $"./images/productos/cesto-jumbo-sin-asas-negro-8595ne.jpg"
        Dim link As String = $"https://www.google.com.mx/?hl=es-419"
        dataTable.Rows.Add(codigo, urlImagen, link, dia, mes, year, idcotizacion)

        Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        processingReport.DataSource = dataTable

    End Sub

End Class