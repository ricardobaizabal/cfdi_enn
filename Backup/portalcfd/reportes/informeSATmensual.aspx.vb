Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Partial Class portalcfd_reportes_informeSATmensual
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle
        lblReportsLegend.Text = Resources.Resource.lblReportsLegend & " - Informe mensual para el SAT"
        If Not IsPostBack Then
            Call CargaCatalogos()
        End If
    End Sub
    Private Sub CargaCatalogos()
        Dim ObjData As New DataControl
        ObjData.Catalogo(mesid, "exec pMisInformes @cmd=1", 0)
        ObjData.Catalogo(annio, "exec pMisInformes @cmd=2", 0)
        ObjData = Nothing
    End Sub

#Region "Events"
    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click


        If Not InformeGenerado() Then


            Dim archivo As String = ""
            Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

            Try

                Dim cmd As New SqlCommand("exec pMisInformes @cmd=3, @mesid='" & mesid.SelectedValue.ToString & "', @annio='" & annio.SelectedValue.ToString & "', @clienteid='" & Session("clienteid").ToString & "'", conn)

                conn.Open()

                Dim rs As SqlDataReader

                rs = cmd.ExecuteReader()

                If rs.Read Then
                    archivo = rs("archivo")
                End If

                rs.Close()

            Catch ex As Exception


            Finally

                conn.Close()
                conn.Dispose()
                conn = Nothing

            End Try
            '
            '
            '   Genera archivo de texto
            '
            Dim fp As StreamWriter
            Try
                fp = File.CreateText(Server.MapPath("~/portalcfd/informes/") & "\" & archivo.ToString)
                Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

                Dim cmdP As New SqlCommand("exec pMisInformes @cmd=5, @mesid='" & mesid.SelectedValue.ToString & "', @annio='" & annio.SelectedValue.ToString & "'", connP)

                connP.Open()

                Dim rsP As SqlDataReader

                rsP = cmdP.ExecuteReader()

                While rsP.Read
                    fp.WriteLine("|" & rsP("rfc") & "|" & rsP("serie") & "|" & rsP("folio").ToString & "|" & rsP("aprobacion").ToString & "|" & rsP("fecha").ToString & "|" & rsP("monto").ToString & "|" & rsP("impuesto").ToString & "|" & rsP("estatus").ToString & "|" & rsP("tipo").ToString & "|" & rsP("numero_pedimento").ToString & "|" & rsP("fecha_pedimento").ToString & "|" & rsP("aduana") & "|")
                End While

                rsP.Close()
                connP.Close()
                connP.Dispose()
                connP = Nothing
                '
                ' Cierra archivo
                '
                fp.Close()
            Catch ex As Exception
                lblMensaje.Text = ex.ToString
            Finally

            End Try
            '
            lblMensaje.Text = ""
            '
        Else
            lblMensaje.Text = "El informe para ese mes y año ya fué generado."
        End If

        Call MuestraInformes()
    End Sub
#End Region

#Region "Grid Handle"
    Private Sub MuestraInformes()
        Dim ObjData As New DataControl
        informeslist.DataSource = ObjData.FillDataSet("exec pMisInformes @cmd=4")
        informeslist.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub informeslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles informeslist.ItemCommand
        Select Case e.CommandName
            Case "cmdDownload"
                Call DescargaInforme(e.CommandArgument)
            Case "cmdDelete"
                Call BorraInforme(e.CommandArgument)
                Call MuestraInformes()
        End Select
    End Sub

    Protected Sub informeslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles informeslist.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
            lnkdel.Attributes.Add("onclick", "return confirm ('Va a eliminar un informe.\n\nEsta acción no borrará los comprobantes.\n\n¿Desea continuar?');")
        End If
    End Sub

    Protected Sub informeslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles informeslist.NeedDataSource
        Dim ObjData As New DataControl
        informeslist.DataSource = ObjData.FillDataSet("exec pMisInformes @cmd=4")
        ObjData = Nothing
    End Sub
#End Region

#Region "Functions"
    Private Sub DescargaInforme(ByVal id As Long)
        Dim archivo As String = ""
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pMisInformes @cmd=6, @informeid='" & id.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                archivo = rs("archivo").ToString
            End If
            '
            rs.Close()
            '
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Dim FilePath = Server.MapPath("~/portalcfd/informes") & "\" & archivo.ToString
        '
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.WriteFile(FilePath)
            Response.End()
        End If
        ''
    End Sub

    Private Function InformeGenerado() As Boolean
        Dim generado As Boolean = False
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pMisInformes @cmd=7, @mesid='" & mesid.SelectedValue.ToString & "', @annio='" & annio.SelectedValue.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                generado = rs("existe")
            End If
            '
            rs.Close()
            '
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        Return generado
        ''
    End Function

    Private Sub BorraInforme(ByVal id As Long)
        Dim archivo As String = ""
        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pMisInformes @cmd=8, @informeid='" & id.ToString & "'", connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                archivo = rs("archivo").ToString
            End If
            '
            rs.Close()
            '
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        '
        Dim FilePath = Server.MapPath("~/portalcfd/informes") & "\" & archivo.ToString
        If File.Exists(FilePath) Then
            File.Delete(FilePath)
        End If
        ''
    End Sub
#End Region

End Class
