﻿<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_almacen_entradas" Codebehind="entradas.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
               <asp:Label ID="lblEntradas" runat="server" Font-Bold="true" CssClass="item" Text="Entradas de Almacen"></asp:Label>
            </legend>
            <br />
            <table border="0" cellpadding="2" cellspacing="0" align="center" width="100%">
                <tr>
                    <td class="item">
                        Escriba el código o alguna palabra clave para encontrar el producto:<br /><br />
                        <asp:TextBox ID="txtSearch" width="250px" runat="server" CssClass="box"></asp:TextBox>&nbsp;<asp:button ID="btnSearch" runat="server" CssClass="boton" Text="buscar" CausesValidation="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Simple" Visible="false">
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Código</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigo" runat="server" Text='<%# eval("codigo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Descripción</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# eval("descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Cant.</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Skin="Default" Width="50px"
                                                MinValue="0" Value='0'>
                                                <NumberFormat DecimalDigits="4" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Costo Unit.</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtCostoUnitario" runat="server" MinValue="0"  Value="0"
                                                Skin="Default" Width="80px">
                                                <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Documento</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDocumento" runat="server" Width="80px" CssClass="box"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>Comentario</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtComentario" TextMode="MultiLine" runat="server" CssClass="box" Width="200px" Height="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="Add">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar entrada de este producto" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
            <br />
        </fieldset>
        <br />
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblProductsListLegend" Text="Ultimos movimientos de entradas a almacen" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="productslist" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="50" GridLines="None"
                            Skin="Simple">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="costo_unitario" HeaderText="Costo unitario" UniqueName="costo_unitario" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="comentario" HeaderText="Comentarios" UniqueName="comentarios">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    
        <br />
        
    
    </telerik:RadAjaxPanel>
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

