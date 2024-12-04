<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="editarorden.aspx.vb" Inherits="LinkiumCFDI.editarorden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
     function OnRequestStart(target, arguments) {
         if ((arguments.get_eventTarget().indexOf("resultslist") > -1) || (arguments.get_eventTarget().indexOf("conceptosList") > -1)) {
             arguments.set_enableAjax(false);
         }
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblEditorOrdenes" runat="server" Font-Bold="true" CssClass="item" Text="Editor de Orden de Compra"></asp:Label>
        </legend>
        <br />        
        <table width="100%" cellspacing="2" cellpadding="2" align="center" style="line-height:25px;">
            <tr>
                <td class="item">
                    <strong>No. Orden: </strong>&nbsp;<asp:Label ID="lblOrden" runat="server" CssClass="item"></asp:Label><br />
                    <strong>Estatus: </strong>&nbsp;<asp:Label ID="lblEstatus" runat="server" CssClass="item"></asp:Label><br />
                    <strong>Proveedor: </strong>&nbsp;<asp:Label ID="lblProveedor" runat="server" CssClass="item"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td class="item">
                    <strong>Comentarios: </strong><br />
                    <telerik:RadTextBox id="txtComentarios" runat="server" TextMode="MultiLine" Width="600px" Height="90px"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" Visible="false" runat="server" Text="Enviar Autorización" />
                </td>
            </tr>
        </table>        
    </fieldset>
    <br />
    <fieldset class="item">
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblPartidas" runat="server" Font-Bold="true" CssClass="item" Text="Conceptos"></asp:Label>
        </legend>
        <br />
        <asp:Panel runat="server" ID="pSeacrh" DefaultButton="btnSearch">
        Producto: &nbsp;<asp:TextBox ID="txtSearch" runat="server" CssClass="item"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CausesValidation="false" />&nbsp;&nbsp;
        <asp:Button id="btnCancel" runat="server" CausesValidation="false" Text="Cancelar búsqueda" />
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="panelSearch" runat="server" Visible="false">
            <telerik:RadGrid ID="resultslist" runat="server" Width="100%" ShowStatusBar="True"
                AutoGenerateColumns="False" AllowPaging="True" PageSize="500" GridLines="None"
                Skin="Default">
                <PagerStyle Mode="NumericPages"></PagerStyle>
                <MasterTableView NoMasterRecordsText="No se encontraron registros." Width="100%" DataKeyNames="productoid" Name="Products" AllowMultiColumnSorting="False">
                    <Columns>
                        <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad" UniqueName="unidad">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="costo_estandar" HeaderText="Costo Estandar" UniqueName="costo_estandar" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Right" HeaderText="Cantidad" UniqueName="cantidad">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtCantidad" Value="0" MinValue="0" runat="server" Width="80px"></telerik:RadNumericTextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" Visible="false" UniqueName="Add">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("productoid") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>
        <br />
        <div align="right" style="height:50px"><br />
        <asp:Button ID="btnAgregaConceptos" runat="server" CssClass="item" Visible="false" Text="Agregar Conceptos" CausesValidation="False" />&nbsp;&nbsp;
        </div>
        <br />
        <asp:Panel runat="server" ID="panelConceptos">
        <telerik:RadGrid ID="conceptosList" runat="server" Width="100%" ShowStatusBar="True"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="20" GridLines="None"
            Skin="Default" ShowFooter="true">
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView NoMasterRecordsText="No se encontraron registros." Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False">
                <Columns>
                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="unidad" HeaderText="Unidad" UniqueName="unidad">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Cantidad" UniqueName="cantidad">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtCantidad" runat="server" Width="80px">
                                <EnabledStyle HorizontalAlign="Right" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="costo_estandar" HeaderText="Costo Estandar" UniqueName="costo_estandar" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="subtotal" HeaderText="Subtotal" UniqueName="subtotal" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="impuesto" HeaderText="Impuesto" UniqueName="impuesto" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total" HeaderText="Total" UniqueName="total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Del">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        </asp:Panel>
        <br />
        <asp:Panel runat="server" ID="panelConceptosRecibidos">
        <telerik:RadGrid ID="conceptosRecibidosList" runat="server" Width="100%" ShowStatusBar="True"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
            Skin="Default" ShowFooter="true">
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False">
                <Columns>
                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad Pedida" UniqueName="cantidad" ItemStyle-HorizontalAlign="Center">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="cantidad_recibida" HeaderText="Cantidad Recibida" UniqueName="cantidad_recibida" ItemStyle-HorizontalAlign="Center">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="costo" HeaderText="Costo (MXN)" UniqueName="costo" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total" HeaderText="Total (MXN)" UniqueName="total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        </asp:Panel>
        <br />
        <br />
        <asp:Button ID="btnAddorder" runat="server" Text="Guardar" CausesValidation="false" />&nbsp;&nbsp;
        <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar orden" CausesValidation="false" />&nbsp;&nbsp;
        <asp:Button ID="btnProcessCancel" runat="server" Text="Cancelar orden" BackColor="Red" ForeColor="White" CausesValidation="false" />&nbsp;&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Regresar al listado" CausesValidation="false" />
        
        <asp:Button ID="btnProcess" runat="server" Text="Procesar orden" Visible="false" CausesValidation="false" />&nbsp;&nbsp;
        <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" Visible="false" CausesValidation="false" />&nbsp;&nbsp;
        <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" Visible="false" CausesValidation="false" />&nbsp;&nbsp;
        
        
        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
        <br /><br />
    </fieldset>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
