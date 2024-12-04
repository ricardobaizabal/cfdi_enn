Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Public Class Conceptos
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        'LoadClaveProdServ(txtClaveProdServ)

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblProductEditLegend.Text = Resources.Resource.lblProductEditLegend

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblCode.Text = Resources.Resource.lblCode
            lblUnit.Text = Resources.Resource.lblUnit
            lblUnitaryPrice.Text = "Precio Unit. 1"
            lblUnitaryPrice2.Text = "Precio Unit. 2"
            lblUnitaryPrice3.Text = "Precio Unit. 3"
            lblDescription.Text = Resources.Resource.lblDescription

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator3.Text = Resources.Resource.validatorMessage
            valClaveUnidad.Text = Resources.Resource.validatorMessage
            valClaveServ.Text = Resources.Resource.validatorMessage
            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddProduct.Text = Resources.Resource.btnAddProduct
            btnSaveProduct.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel

            Dim objCat As New DataControl
            objCat.Catalogo(tasaid, "select id, nombre from tblTasa order by id", 0)
            objCat.Catalogo(monedaid, "select id, nombre from tblMoneda order by id", 0)
            objCat.Catalogo(unidadid, "exec pCatalogos @cmd=7", 0)
            objCat.Catalogo(cboProductoServ, "select id, isnull(nombre,'') as nombre from tblClaveProducto order by nombre", 0)
            objCat.Catalogo(cbmObjetoImpuesto, "select id, descripcion from tblObjetoImp", 0)
            objCat = Nothing
        End If

    End Sub

    Function ObtenerClaveProveedor() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogos @cmd=4", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

    Function ObtenerClaveUnidad() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogos @cmd=5", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

#End Region

#Region "Load List Of Products"

    Function GetProducts() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pMisProductos @cmd=1, @clienteid='" & Session("clienteid") & "', @txtSearch='" & txtSearch.Text & "'", conn)
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

#End Region

#Region "Telerik Grid Products Loading Events"

    Protected Sub productslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles productslist.NeedDataSource

        If Not e.IsFromDetailTable Then

            productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
            productslist.DataSource = GetProducts()

        End If

    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub productslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles productslist.Init

        productslist.PagerStyle.NextPagesToolTip = "Ver mas"
        productslist.PagerStyle.NextPageToolTip = "Siguiente"
        productslist.PagerStyle.PrevPagesToolTip = "Ver más"
        productslist.PagerStyle.PrevPageToolTip = "Atrás"
        productslist.PagerStyle.LastPageToolTip = "Última Página"
        productslist.PagerStyle.FirstPageToolTip = "Primera Página"
        productslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        productslist.SortingSettings.SortToolTip = "Ordernar"
        productslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        productslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = productslist.FilterMenu
        Dim i As Integer = 0

        While i < menu.Items.Count

            If menu.Items(i).Text = "NoFilter" Or menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If

        End While

        Call ModificaIdiomaGrid()

    End Sub

    Private Sub ModificaIdiomaGrid()

        productslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = productslist.FilterMenu
        Dim item As Telerik.Web.UI.RadMenuItem

        For Each item In Menu.Items

            ''''''''''''''''''''''''''''''''''''''''''''''
            'Change The Text For The StartsWith Menu Item'
            ''''''''''''''''''''''''''''''''''''''''''''''

            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "Contains" Then
                item.Text = "Contiene"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If

        Next

    End Sub

#End Region

#Region "Telerik Grid Products Editing & Deleting Events"

    Protected Sub productslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Products" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ProductsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub productslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles productslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditProduct(e.CommandArgument)

            Case "cmdDelete"
                DeleteProduct(e.CommandArgument)

        End Select

    End Sub

    Private Sub DeleteProduct(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisProductos  @cmd='2', @productoId ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelProductRegistration.Visible = False

            productslist.DataSource = GetProducts()
            productslist.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub EditProduct(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisProductos @cmd='4', @productoid='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtCode.Text = rs("codigo")
                txtUnitaryPrice.Text = rs("unitario")
                txtUnitaryPrice2.Text = rs("unitario2")
                txtUnitaryPrice3.Text = rs("unitario3")
                txtUnitaryPrice4.Text = rs("unitario4")
                txtDescription.Text = rs("descripcion")
                txtCostoStd.Text = rs("costo_estandar")
                txtIEPS.Text = rs("ieps")

                panelProductRegistration.Visible = True

                InsertOrUpdate.Value = 1
                ProductID.Value = id

                Dim objCat As New DataControl
                objCat.Catalogo(tasaid, "select id, nombre from tblTasa order by id", rs("tasaid"))
                objCat.Catalogo(monedaid, "select id, nombre from tblMoneda order by id", rs("monedaid"))
                objCat.Catalogo(unidadid, "exec pCatalogos @cmd=7", rs("unidadid"))
                objCat.Catalogo(cboproductoserv, "select id, isnull(nombre,'') as nombre from tblClaveProducto order by nombre", rs("claveproductoid"))
                cbmObjetoImpuesto.SelectedValue = rs("objeto_impuestoid")
                objCat = Nothing

                If String.IsNullOrEmpty(rs("foto")) = True Then
                    imgProducto.Visible = False
                    hdnFoto.Value = ""
                    imgBtnEliminarAnexo.Visible = False
                Else
                    imgProducto.Visible = True
                    imgBtnEliminarAnexo.Visible = True
                    foto.Visible = False
                    imgProducto.ImageUrl = "~/images/productos/" & rs("foto").ToString
                    hdnFoto.Value = rs("foto")
                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

#End Region

#Region "Telerik Grid Products Column Names (From Resource File)"

    Protected Sub productslist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles productslist.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

            If e.Item.OwnerTableView.Name = "Products" Then

                'header("codigo").Text = Resources.Resource.gridColumnNameCode
                header("unidad").Text = Resources.Resource.gridColumnNameMeasureUnit
                header("descripcion").Text = Resources.Resource.gridColumnNameDescription
                header("unitario").Text = Resources.Resource.gridColumnNameUnitaryPrice
                header("unitario2").Text = Resources.Resource.gridColumnNameUnitaryPrice2
                header("unitario3").Text = Resources.Resource.gridColumnNameUnitaryPrice3

            End If

        End If

    End Sub

#End Region

#Region "Display Product Data Panel"

    Protected Sub btnAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProduct.Click

        InsertOrUpdate.Value = 0

        txtCode.Text = ""
        unidadid.SelectedValue = 0
        txtUnitaryPrice.Text = ""
        txtUnitaryPrice2.Text = ""
        txtUnitaryPrice3.Text = ""
        txtDescription.Text = ""
        unidadid.SelectedValue = 0
        cboproductoserv.SelectedValue = 0
        unidadid.SelectedValue = 0
        cbmObjetoImpuesto.SelectedValue = 0
        panelProductRegistration.Visible = True

    End Sub

#End Region

#Region "Save Product"

    Protected Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveProduct.Click
        Dim ObjData As New DataControl
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            If InsertOrUpdate.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pMisProductos @cmd=3, @codigo='" & txtCode.Text & "', @unitario='" & txtUnitaryPrice.Text & "', @unitario2='" & txtUnitaryPrice2.Text & "', @unitario3='" & txtUnitaryPrice3.Text & "', @unitario4='" & txtUnitaryPrice4.Text & "', @descripcion='" & txtDescription.Text & "', @tasaid='" & tasaid.SelectedValue.ToString & "', @costo_estandar='" & txtCostoStd.Text & "', @monedaid='" & monedaid.SelectedValue.ToString & "', @claveproductoid='" & cboProductoServ.SelectedValue & "', @unidadid='" & unidadid.SelectedValue & "', @ieps='" & txtIEPS.Text.ToString & "', @foto='" & actualizaFoto.ToString & "', @objeto_impuestoid = '" & cbmObjetoImpuesto.SelectedValue & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelProductRegistration.Visible = False

                productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                productslist.DataSource = GetProducts()
                productslist.DataBind()

                conn.Close()
                conn.Dispose()

            Else

                Dim cmd As New SqlCommand("EXEC pMisProductos @cmd=5, @productoid='" & ProductID.Value & "', @codigo='" & txtCode.Text & "', @unitario='" & txtUnitaryPrice.Text & "', @unitario2='" & txtUnitaryPrice2.Text & "', @unitario3='" & txtUnitaryPrice3.Text & "', @unitario4='" & txtUnitaryPrice4.Text & "', @descripcion='" & txtDescription.Text & "', @tasaid='" & tasaid.SelectedValue.ToString & "', @costo_estandar='" & txtCostoStd.Text & "', @monedaid='" & monedaid.SelectedValue.ToString & "', @claveproductoid='" & cboProductoServ.SelectedValue & "',@unidadid='" & unidadid.SelectedValue & "', @ieps='" & txtIEPS.Text.ToString & "', @foto='" & actualizaFoto.ToString & "', @objeto_impuestoid = '" & cbmObjetoImpuesto.SelectedValue & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panelProductRegistration.Visible = False

                productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                productslist.DataSource = GetProducts()
                productslist.DataBind()

                conn.Close()
                conn.Dispose()

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        ObjData = Nothing

    End Sub

    Private Function actualizaFoto() As String
        '
        '   Actualiza o Agregar Foto
        '
        Dim archivo As String = ""
        Dim i As Integer = 0
        '
        If hdnFoto.Value.ToString = "" Then
            If foto.PostedFile.ContentLength > 0 Then
                archivo = foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                If File.Exists(Server.MapPath("~/images/productos/") + archivo) Then
                    For i = 1 To 999
                        archivo = i.ToString + "_" + foto.PostedFile.FileName.Substring(foto.PostedFile.FileName.LastIndexOf("\") + 1)
                        If Not File.Exists(Server.MapPath("~/images/productos/") + archivo) Then
                            foto.PostedFile.SaveAs(Server.MapPath("~/images/productos/") + archivo)
                            Exit For
                        End If
                    Next
                Else
                    foto.PostedFile.SaveAs(Server.MapPath("~/images/productos/") + archivo)
                End If
            Else
                archivo = hdnFoto.Value.ToString
            End If
        Else
            archivo = hdnFoto.Value.ToString
        End If

        Return archivo

    End Function

#End Region

#Region "Cancel Product (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        InsertOrUpdate.Value = 0

        txtCode.Text = ""
        unidadid.SelectedValue = 0
        cboproductoserv.SelectedValue = 0
        tasaid.SelectedValue = 0
        monedaid.SelectedValue = 0
        txtUnitaryPrice.Text = ""
        txtUnitaryPrice2.Text = ""
        txtUnitaryPrice3.Text = ""
        txtDescription.Text = ""
        txtCostoStd.Text = ""
        txtIEPS.Text = ""

        panelProductRegistration.Visible = False

    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        productslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ProductsEmptyGridMessage
        productslist.DataSource = GetProducts()
        productslist.DataBind()
    End Sub

    Private Sub imgBtnEliminarAnexo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEliminarAnexo.Click
        foto.Visible = True
        imgProducto.Visible = False
        imgBtnEliminarAnexo.Visible = False
        hdnFoto.Value = ""
    End Sub

End Class