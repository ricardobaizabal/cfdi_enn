Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.Globalization
Imports FirmaSAT.Sat
Imports System.IO
Imports System.Xml.Serialization
Partial Class portalcfd_CFD_Detalle
    Inherits System.Web.UI.Page
    Private serie As String = ""
    Private folio As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call CargaDetalleCobranzaCFD()
        End If
    End Sub

    Private Sub CargaDetalleCobranzaCFD()
        
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try


            Dim cmd As New SqlCommand("EXEC pMisInformes @cmd=13, @cfdid='" & Request("id").ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                lblDocumento.Text = rs("serie").ToString & rs("folio").ToString
                Dim ObjData As New DataControl
                ObjData.Catalogo(estatus_cobranzaid, "exec pMisInformes @cmd=11", rs("estatus_cobranzaId"))
                ObjData.Catalogo(tipo_pagoid, "exec pMisInformes @cmd=12", rs("tipo_pagoId"))
                ObjData = Nothing
                referencia.Text = rs("referencia")
                If Not IsDBNull(rs("fecha_pago")) Then
                    fechapago.SelectedDate = rs("fecha_pago")
                End If
            End If
            rs.Close()
            '
        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        '
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '
        Dim ObjData As New DataControl
        If Not fechapago.SelectedDate Is Nothing Then
            ObjData.RunSQLQuery("exec pMisInformes @cmd=14, @estatus_cobranzaId='" & estatus_cobranzaid.SelectedValue.ToString & "', @tipo_pagoId='" & tipo_pagoid.SelectedValue.ToString & "', @referencia='" & referencia.Text & "', @cfdid='" & Request("id").ToString & "', @fecha_pago='" & fechapago.SelectedDate.Value.ToShortDateString & "'")
        Else
            ObjData.RunSQLQuery("exec pMisInformes @cmd=14, @estatus_cobranzaId='" & estatus_cobranzaid.SelectedValue.ToString & "', @tipo_pagoId='" & tipo_pagoid.SelectedValue.ToString & "', @referencia='" & referencia.Text & "', @cfdid='" & Request("id").ToString & "'")
        End If
        ObjData = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        If System.Configuration.ConfigurationManager.AppSettings("usuarios") = 1 Then
            Call ActualizaCFDUsuario(Request("id"))
        End If
        '
        Response.Redirect("~/portalcfd/reportes/ingresos.aspx")
    End Sub

    Private Sub ActualizaCFDUsuario(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pUsuarios @cmd=8, @userid='" & Session("userid").ToString & "', @cfdid='" & cfdid.ToString & "'")
        ObjData = Nothing
    End Sub
End Class
