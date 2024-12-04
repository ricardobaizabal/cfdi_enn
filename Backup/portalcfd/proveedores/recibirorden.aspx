<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="recibirorden.aspx.vb" Inherits="LinkiumCFDI.recibirorden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function OnClientClose(sender, args) {
            document.location.href = document.location.href;
        }
                
        function openRadWindow(id) {
            var radwindow = $find('<%=ReceiveWindow.ClientID %>');
            radwindow.setUrl("recibir.aspx?id=" + id);
            radwindow.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblEditorOrdenes" runat="server" Font-Bold="true" CssClass="item" Text="Recibir Orden de Compra"></asp:Label>
        </legend>
        <br />
        <table width="100%" cellspacing="2" cellpadding="2" align="center" style="line-height:25px;">
            <tr>
                <td class="item">
                    <strong>No. Orden: </strong>&nbsp;<asp:Label ID="lblOrden" runat="server" CssClass="item"></asp:Label><br />
                    <strong>Estatus: </strong>&nbsp;<asp:Label ID="lblEstatus" runat="server" CssClass="item"></asp:Label><br />
                    <strong>Proveedor: </strong>&nbsp;<asp:Label ID="lblProveedor" runat="server" CssClass="item"></asp:Label><br />
                    <%--<strong>Tasa: </strong>&nbsp;<asp:Label ID="lblTasa" runat="server" CssClass="item"></asp:Label><br />--%>
                </td>
            </tr>
            <tr>
                <td class="item">
                    <strong>Comentarios: </strong><br />
                    <telerik:RadTextBox id="txtComentarios" runat="server" TextMode="MultiLine" Width="600px" Height="90px"></telerik:RadTextBox>
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
        <telerik:RadGrid ID="conceptosList" runat="server" Width="100%" ShowStatusBar="True"
            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
            Skin="Default" ShowFooter="true">
            <PagerStyle Mode="NumericPages"></PagerStyle>
            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False">
                <Columns>
                    <telerik:GridBoundColumn DataField="codigo" HeaderText="Código" UniqueName="codigo">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripción" UniqueName="descripcion">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad" UniqueName="cantidad" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="cantidad_recibida" HeaderText="Cantidad Recibida" UniqueName="cantidad_recibida" ItemStyle-HorizontalAlign="Center">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="costo" HeaderText="Costo (MXN)" UniqueName="costo" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="total" HeaderText="Total (MXN)" UniqueName="total" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                    </telerik:GridBoundColumn>                        
                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Add" Visible="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <br />
        <br /><br />
        <asp:Button ID="btnCancelar" runat="server" CssClass="item" Text="Regresar al listado" CausesValidation="false" />
        <br /><br />
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
        <br /><br />
    </fieldset>    
    <br /><br /><br />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Default" VisibleStatusbar="False" Behavior="Default" Height="80px" Style="z-index: 8000">
        <Windows> 
            <telerik:RadWindow ID="ReceiveWindow" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" VisibleStatusbar="False" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close" Width="600px" Height="550px" Skin="Default" OnClientClose="OnClientClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
