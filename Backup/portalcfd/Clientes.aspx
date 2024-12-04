<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_Clientes" CodeBehind="Clientes.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4 {
            height: 17px;
        }

        .style5 {
            height: 14px;
        }

        .style6 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <asp:Panel ID="pBusqueda" runat="server" DefaultButton="btnSearch" >
            <br />
            <span class="item">Palabra clave:
                <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="boton" Text="Buscar" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CssClass="boton" Text="Ver todo" />
            </span>
            <br />
            <br />
            </asp:Panel>
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblClientsListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="clientslist" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                            PageSize="15" ShowStatusBar="True"
                            Skin="Simple" Width="100%">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="id"
                                Name="Clients" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="No. Cliente" UniqueName="id"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Razón Social" DataField="razonsocial" SortExpression="razonsocial" UniqueName="razonsocial">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("razonsocial") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="contacto" HeaderText="Contacto" UniqueName="contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="telefono_contacto" HeaderText="Teléfono" UniqueName="telefono_contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server"
                                                CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete"
                                                ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAddClient" runat="server" CausesValidation="False"
                            CssClass="item" TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px"></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelClientRegistration" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblClientEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Simple" MultiPageID="RadMultiPage1" SelectedIndex="0" CausesValidation="False">
                    <Tabs>
                        <telerik:RadTab Text="Datos Generales" TabIndex="0" Value="0" Enabled="True" Selected="true">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Cuentas Bancarias" TabIndex="1" Value="1" Enabled="False">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Contactos Adicionales" TabIndex="1" Value="2" >
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" Selected="true">   
                        <table width="100%" border="0">
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
	                                <asp:Label ID="Label4" runat="server" CssClass="item" Font-Bold="True" Text="Denominación/Razón Social:"/>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblTipoPrecio" runat="server" Text="Tipo Precio:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtSocialReason" runat="server" Width="92%">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="33%">
	                                <telerik:RadTextBox ID="txtDenominacionRaznScial" runat="server" Width="85%"></telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <asp:DropDownList ID="cmbTipoPrecio" runat="server" CssClass="box">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ControlToValidate="txtSocialReason" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" SetFocusOnError="true" ControlToValidate="cmbTipoPrecio" ErrorMessage="Requerido" InitialValue="0" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContactEmail" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContact" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContactEmail" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContactPhone" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">&nbsp;</td>
                                <td class="style4" width="33%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtContactEmail" CssClass="item"
                                        ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                                </td>
                                <td class="style4" width="33%"></td>
                            </tr>
                            <tr>
                                <td width="33%" class="style5"></td>
                                <td width="33%" class="style5"></td>
                                <td width="33%" class="style5"></td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblStreet" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%" align="left">
                                    <asp:Label ID="lblExtNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblIntNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="left" width="33%">
                                    <asp:Label ID="lblColony" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtStreet" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtExtNumber" runat="server" Width="35%">
                                    </telerik:RadTextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <telerik:RadTextBox ID="txtIntNumber" runat="server" Width="35%">
                                </telerik:RadTextBox>
                                </td>
                                <td align="left" width="33%">
                                    <telerik:RadTextBox ID="txtColony" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" ControlToValidate="txtStreet" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="true" ControlToValidate="txtExtNumber" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left" width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="true" ControlToValidate="txtColony" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblCountry" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblState" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblTownship" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:DropDownList ID="paisid" runat="server" CssClass="box" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td width="33%">
                                    <asp:DropDownList ID="estadoid" runat="server" CssClass="box" AutoPostBack="true"></asp:DropDownList>
                                    <telerik:RadTextBox ID="txtStates" runat="server" Width="85%" Visible="false"></telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtTownship" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" SetFocusOnError="true" ControlToValidate="paisid" CssClass="item" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" SetFocusOnError="true" ControlToValidate="estadoid" InitialValue="0" CssClass="item"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" SetFocusOnError="true" ControlToValidate="txtStates" CssClass="item" Enabled="false"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true" ControlToValidate="txtTownship" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblZipCode" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblCondiciones" runat="server" CssClass="item" Font-Bold="true" Text="Condiciones:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtZipCode" runat="server" Width="85%" Visible="false">
                                    </telerik:RadTextBox>
                                    <telerik:RadAutoCompleteBox TextSettings-SelectionMode="Single" runat="server" ID="txtZipCod"
                                        DataTextField="codigo" DataValueField="id" InputType="Text" Width="290px" DropDownWidth="150px">
                                        <TokensSettings AllowTokenEditing="true" />
                                    </telerik:RadAutoCompleteBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtRFC" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="condicionesid" runat="server" CssClass="box"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="valZipCode1" runat="server" ControlToValidate="txtZipCode" SetFocusOnError="true" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="valZipCode2" runat="server" Enabled="false" InitialValue="" ControlToValidate="txtZipCod" SetFocusOnError="true" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" SetFocusOnError="true" ControlToValidate="txtRFC" CssClass="item"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valRFC" CssClass="item" runat="server" SetFocusOnError="true" ControlToValidate="txtRFC" ValidationExpression="^([a-zA-Z&]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblContribuyente" runat="server" Text="Tipo de contribuyente / Honorarios" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFormaPago" runat="server" CssClass="item" Font-Bold="true" Text="Forma de pago:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNumCtaPago" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="tipoContribuyenteid" runat="server" CssClass="box" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="formapagoid" runat="server" CssClass="box"></asp:DropDownList>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNumCtaPago" runat="server" Width="55%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="valTipoContribuyente" runat="server" SetFocusOnError="true" InitialValue="0" ControlToValidate="tipoContribuyenteid" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblRegimen" runat="server" CssClass="item" Font-Bold="true" Text="Regimen fiscal"></asp:Label>
                                </td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:DropDownList ID="regimenid" CssClass="box" Width="85%" runat="server"></asp:DropDownList>
                                </td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="valRegimen" runat="server" InitialValue="0" ControlToValidate="regimenid" CssClass="item" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido" ></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="3">
                                    <asp:Button ID="btnSaveClient" runat="server" CssClass="item" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="item" CausesValidation="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 66%; height: 5px;">
                                    <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                                    <asp:HiddenField ID="ClientsID" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                    <br />
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="Label6" runat="server" Text="Agregar / Editar Cuentas Bancarias" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td colspan="7">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    <asp:Label ID="lblBanco" runat="server" CssClass="item" Font-Bold="True" Text="Banco Nacional:"></asp:Label>
                                </td>
                                <td style="width:30%;">
                                    <asp:Label ID="lblBancoExtr" runat="server" CssClass="item" Font-Bold="True" Text="Banco Extranjero:"></asp:Label>
                                </td>
                                <td style="width:30%;">
                                    <asp:Label ID="lblRfc1" runat="server" CssClass="item" Font-Bold="True" Text="RFC:"></asp:Label>
                                </td>
                                <td style="width:10%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    <telerik:RadTextBox ID="txtBanco" runat="server" Width="95%">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width:30%;">
                                    <telerik:RadTextBox ID="txtBancoExtr" runat="server" Width="95%">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width:30%;">
                                    <telerik:RadTextBox ID="txtRFCBAK" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width:10%;"></td>
                            </tr>
                            <tr>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="vDatosCuenta" ControlToValidate="txtRFCBAK" SetFocusOnError="True" CssClass="item" Text="Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="item" runat="server" ValidationGroup="vDatosCuenta" ControlToValidate="txtRFCBAK" SetFocusOnError="True" ValidationExpression="^([a-zA-Z&]{3,4})\d{6}([a-zA-Z\w]{3})$" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    <asp:Label ID="lblNonCuenta" runat="server" CssClass="item" Font-Bold="True" Text="Número de Cuenta:"></asp:Label>
                                </td>
                                <td style="width:30%;">
                                    <asp:Label ID="Label7" runat="server" CssClass="item" Font-Bold="True" Text="Predeterminado:"></asp:Label>
                                </td>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:10%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    <telerik:RadTextBox ID="txtCuenta" runat="server" Width="96%">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width:30%;">
                                    <asp:CheckBox runat="server" ID="chkPredeterminado" />
                                </td>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:10%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    <asp:RequiredFieldValidator ID="valCuenta" runat="server" SetFocusOnError="true" ControlToValidate="txtCuenta" ValidationGroup="vDatosCuenta" Text="Requerido" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">
                                    <asp:Button ID="btnGuardar" runat="server" ValidationGroup="vDatosCuenta" Text="Guardar" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="width: 66%; height: 5px;">
                                    <asp:HiddenField ID="CuentaID" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblSucursalesListLegend" runat="server" Text="Listado de Cuentas Bancarias" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td style="height: 5px">
                                    <telerik:RadGrid ID="cuentasList" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" GridLines="None"
                                        PageSize="20" ShowStatusBar="True"
                                        Skin="Simple" Width="100%">
                                        <PagerStyle Mode="NumericPages" />
                                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Cuentas" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="banconacional" HeaderText="Banco Nacional" UniqueName="banconacional">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="bancoextranjero" HeaderText="Banco Extranjero" UniqueName="bancoextranjero">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="numctapago" HeaderText="Cuenta Bancaria" UniqueName="numctapago">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Predeterminado" UniqueName="">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgPredeterminado" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/arrow.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                    <br />
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="Label1" runat="server" Text="Agregar / Editar Cuentas Bancarias" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td colspan="7">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="True" Text="Nombre:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="valNombreContAdicional" runat="server" ValidationGroup="valContAdicional" ControlToValidate="txtNombreContAdicional" SetFocusOnError="True" CssClass="item" Text="Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width:30%;">
                                    <asp:Label ID="Label3" runat="server" CssClass="item" Font-Bold="True" Text="Correo:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="valCorreoContAdicional" runat="server" ValidationGroup="valContAdicional" ControlToValidate="txtCorreoContAdicional" SetFocusOnError="True" CssClass="item" Text="Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regCorreoContAdicional" CssClass="item" runat="server" ControlToValidate="txtCorreoContAdicional" 
                                    Text="Formato no válido" ForeColor="#c10000" SetFocusOnError="True" ValidationGroup="valContAdicional" ValidationExpression="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width:30%;">
                                </td>
                                <td style="width:10%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:30%;">
                                    <telerik:RadTextBox ID="txtNombreContAdicional" runat="server" Width="95%">
                                    </telerik:RadTextBox>
                                   
                                </td>
                                <td style="width:30%;">
                                    <telerik:RadTextBox ID="txtCorreoContAdicional" runat="server" Width="95%" >
                                    </telerik:RadTextBox>
                                    
                                </td>
                                <td style="width:30%;">
                                </td>
                                <td style="width:10%;"></td>
                            </tr>
                            <tr>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">&nbsp;</td>
                                <td style="width:30%;">
                                    <asp:HiddenField ID="ContactoAdicionalID" Value="0" runat="server" />
                                    <asp:Button ID="btnSaveContAdicional" runat="server" Text="Guardar" ValidationGroup="valContAdicional" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelContAdicional" runat="server" Text="Cancelar" Visible="false" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="Label9" runat="server" Text="Listado de Contactos Adicionales" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td style="height: 5px">
                                    <telerik:RadGrid ID="contactosadicionaleslist" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" GridLines="None"
                                        PageSize="20" ShowStatusBar="True"
                                        Skin="Simple" Width="100%">
                                        <PagerStyle Mode="NumericPages" />
                                        <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Cuentas" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="nombre" HeaderText="Nombre" UniqueName="nombre">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="correo" HeaderText="Correo" UniqueName="correo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
                </telerik:RadMultiPage>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>