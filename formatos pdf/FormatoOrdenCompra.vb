Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.Reporting
Imports Telerik.Reporting.Drawing
Imports System.Configuration
Imports System.Data.SqlClient

Partial Public Class FormatoOrdenCompra
    Inherits Telerik.Reporting.Report

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FormatoOrdenCompra_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource
        Dim ds As New DataSet
        ds = FillDataSet("exec pOrdenCompra @cmd=3, @ordenId='" & Me.ReportParameters("pIdOrdenCompra").Value.ToString & "'")

        Me.txtOrdenCompra.Value = "Orden Compra No. " & Me.ReportParameters("pIdOrdenCompra").Value.ToString

        If ds.Tables(0).Rows.Count > 0 Then
            Me.txtOrdenCompra.Value = Me.ReportParameters("pIdOrdenCompra").Value.ToString
            Me.txtFecha.Value = ds.Tables(0)(0)("fecha").ToString
            Me.txtProveedor.Value = ds.Tables(0)(0)("proveedor").ToString
        End If

        ds = FillDataSet("exec pOrdenCompra @cmd=7, @ordenId='" & Me.ReportParameters("pIdOrdenCompra").Value.ToString & "'")

        Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
        processingReport.DataSource = ds

        If ds.Tables(0).Rows.Count > 0 Then
            Me.txtCostoTotal.Value = FormatCurrency(ds.Tables(0).Compute("SUM(Subtotal)", ""), 2)
        Else
            Me.txtCostoTotal.Value = FormatCurrency(0, 2)
        End If
    End Sub

    Public Function FillDataSet(ByVal SQL As String) As DataSet
        Dim p_conexion = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim conn As New SqlConnection(p_conexion)
        conn.Open()
        Dim cmd As New SqlDataAdapter(SQL, conn)
        Dim ds As DataSet = New DataSet
        cmd.Fill(ds)
        conn.Close()
        conn.Dispose()
        conn = Nothing
        Return ds
    End Function

End Class