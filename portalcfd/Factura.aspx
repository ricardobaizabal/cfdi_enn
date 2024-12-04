<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeFile="Factura.aspx.vb" Inherits="portalcfd_Factura" MaintainScrollPositionOnPostback="true" Debug="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
    
        function validateCombo(source, args)
        {
           args.IsValid = false;

           var combo = $find("<%= cmbCode.ClientID %>");
           var text = combo.get_text();

           if (text.length < 1)
           {
               args.IsValid = false;
           }

           else
           {
               args.IsValid = true;
           }
       }

    </script>

    <style type="text/css">
        .style3
        {
            height: 3px;
        }
        .titulos 
        {
            font-family:verdana;
            font-size:medium;
            color:Purple;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    
        <br />

        <asp:Panel ID="panelClients" runat="server">

            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblClientsSelectionLegend" runat="server" Font-Bold="true" CssClass="titulos"></asp:Label>
                </legend>

                <br />

                <telerik:RadComboBox ID="cmbClient" runat="server" AllowCustomText="True" CausesValidation="False"
                    CssClass="item" Width="50%" AutoPostBack="true">
                </telerik:RadComboBox>            
                
                <br /><br />
                <span class="item">
                    Tipo de documento: <asp:DropDownList ID="serieid" runat="server" 
                    CssClass="box" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="valSerieId" runat="server" InitialValue="0" ErrorMessage="Seleccione la serie que utilizará para facturar." ControlToValidate="serieid" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                    
                    IVA: <asp:DropDownList ID="tasaid" runat="server" CssClass="box" 
                    AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="valTasaid" runat="server" InitialValue="0" ErrorMessage="Seleccione la tasa de IVA que utilizará para facturar." ControlToValidate="serieid" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </span>
                <br /><br />
                <asp:panel ID="panelDivisas" runat="server">
                    <span class="item">
                        Tipo de cambio: $ <telerik:RadNumericTextBox ID="tipocambio" runat="server" NumberFormat-DecimalDigits="2" Value="0"></telerik:RadNumericTextBox>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="valTipoCambio" runat="server" ControlToValidate="tipocambio" ErrorMessage="Escriba el tipo de cambio" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </span>
                </asp:panel>
            </fieldset>

        </asp:Panel>

        <br />    

        <asp:Panel ID="panelSpecificClient" runat="server" Visible="False">

            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblClientData" runat="server" Font-Bold="true" CssClass="titulos"></asp:Label>
                </legend>

                <br />

                <table width="100%">
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblCondiciones" runat="server" CssClass="item" Font-Bold="True" Text="Condiciones"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblSocialReasonValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblContactValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblCondicionesValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblTipoPrecio" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Precio:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblContactPhoneValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblRFCValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                            <asp:Label ID="lblTipoPrecioValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblEnviar" runat="server" CssClass="item" 
                                Font-Bold="True" Text="Enviar a:"></asp:Label>
                        </td>
                        <td width="33%">
                            
                        </td>
                        <td width="33%">
                            
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" width="66%">
                            <asp:TextBox ID="enviara" runat="server" Width="450px" CssClass="box"></asp:TextBox>
                        </td>
                        <td width="33%">
                            
                        </td>
                    </tr>
                    <asp:Panel ID="panelInstrucciones" runat="server" Visible="true">
                    <tr>
                        <td width="33%">
                            <asp:Label ID="lblInstruccionesEspeciales" runat="server" CssClass="item" 
                                Font-Bold="True" Text="Instrucciones Especiales y/o Consignado a:"></asp:Label>
                        </td>
                        <td width="33%">
                            
                        </td>
                        <td width="33%">
                            
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" width="66%">
                            <asp:TextBox ID="instrucciones" runat="server" TextMode="MultiLine" Height="40px" Width="450px" CssClass="box"></asp:TextBox>
                        </td>
                        <td width="33%">
                            
                        </td>
                    </tr>
                    </asp:Panel>
                    
                    <tr>
                        <td width="33%">
                            <asp:CheckBox Visible="true" CssClass="item" ID="chkAduana" runat="server" Text="Incluye información aduanera" AutoPostBack="true" TextAlign="Right" />
                        </td>
                        <td width="33%">
                            
                        </td>
                        <td width="33%">
                            
                        </td>
                    </tr>
                    <asp:Panel ID="panelInformacionAduanera" runat="server" Visible ="false">
                        <tr>
                            <td colspan="3" class="item" style="line-height:20px;">
                                <strong>Nombre de la aduana:</strong> <asp:RequiredFieldValidator ID="valNombreAduana" runat="server" ControlToValidate="nombreaduana" ErrorMessage="Escriba el nombre de la aduana." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="nombreaduana" Width="450px" runat="server" CssClass="box"></asp:TextBox>
                                <br />
                                <strong>Fecha de pedimento:</strong> <asp:RequiredFieldValidator ID="valFechaPedimento" runat="server" ControlToValidate="fechapedimento" ErrorMessage="Selecciona la fecha del pedimento." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <telerik:RadDatePicker ID="fechapedimento" runat="server">
                                </telerik:RadDatePicker><br />
                                <strong>Número de pedimento:</strong> <asp:RequiredFieldValidator ID="valNumeroPedimento" runat="server" ControlToValidate="numeropedimento" ErrorMessage="Escriba el número de pedimento." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="numeropedimento" Width="450px" runat="server" CssClass="box"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                    </asp:Panel>
                    
                </table>

            </fieldset>

        </asp:Panel>

        <br />

        <asp:Panel ID="panelItemsRegistration" runat="server" Visible="False">

            <fieldset>
                <asp:HiddenField ID="productoid" runat="server" />
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblClientItems" runat="server" Font-Bold="true" CssClass="titulos"></asp:Label>
                </legend>

                <br />

                <table width="550" cellspacing="0" cellpadding="1" border="0" align="center">
                    <tr>
                        <td  valign="middle" colspan="3">
                            <asp:Label ID="lblCode" runat="server" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;<asp:CustomValidator ID="RequiredFieldValidator1" runat="server" CssClass="item"
                               ClientValidationFunction="validateCombo">
                            </asp:CustomValidator>
                            <br />
                            <telerik:RadComboBox ID="cmbCode" runat="server" Width="486px" CssClass="item" AllowCustomText="true" 
                                                 CausesValidation="false" MarkFirstMatch="true" 
                                ShowToggleImage="false" AutoPostBack="true" ShowDropDownOnTextboxClick="False">
                            </telerik:RadComboBox>
                            <asp:Label ID="lblSearchResult" runat="server" CssClass="item" 
                                Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                        
                        
                    </tr>
                    
                    <tr>
                        <td colspan="3" valign="middle">
                            <asp:Label ID="lblDescription" runat="server" CssClass="item" Font-Bold="True"></asp:Label>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtDescription" CssClass="item"></asp:RequiredFieldValidator>
                            <br />
                            <telerik:RadTextBox ID="txtDescription" Runat="server" Width="484px">
                            </telerik:RadTextBox>
                            <br /><div style="height:6px;"></div>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="left" valign="bottom">
                            <asp:Label ID="lblQuantity" runat="server" CssClass="item" Font-Bold="True"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtQuantity" CssClass="item"></asp:RequiredFieldValidator> <br />
                            <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Skin="Default" Width="120px"
                                MinValue="0" Value="0">
                                <NumberFormat DecimalDigits="4" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="183" align="left" valign="bottom">
                            <asp:Label ID="lblUnit" runat="server" CssClass="item" Font-Bold="True"></asp:Label><br />
                            <telerik:RadTextBox ID="txtUnit" Runat="server" Width="120px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="183" align="left" valign="bottom">
                            <asp:Label ID="lblUnitaryPrice" runat="server" CssClass="item" Font-Bold="True"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtUnitaryPrice" CssClass="item"></asp:RequiredFieldValidator><br />
                            <telerik:RadNumericTextBox ID="txtUnitaryPrice" runat="server" MinValue="0" Value="0"
                                Skin="Default" Width="120px">
                                <NumberFormat DecimalDigits="4" GroupSeparator="," />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                   
                   
                    <tr>
                        <td colspan="3">
                            <asp:CheckBox ID="chkDescuento" runat="server" 
                                Text="Esta partida es un descuento" Font-Bold="true" 
                                CssClass="item" Visible="true" />
                        </td>
                    </tr>
                   
                   
                    <tr>
                        <td colspan="3"><asp:RangeValidator id="valCantidadRango" runat="server" ControlToValidate="txtQuantity" SetFocusOnError="True" Type="Double" ErrorMessage="Especifique la cantidad" MinimumValue="0.1" MaximumValue="1000000"></asp:RangeValidator></td>
                    </tr>
                    
                    
                    <tr><td colspan="3"><asp:Button ID="btnAddItem" runat="server" CssClass="item" TabIndex="6" /><asp:Label ID="lblMensaje" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></td></tr>
                    </table>
                    <br /><br />
                    <table width="889" cellspacing="0" cellpadding="1" border="0" align="center">
                    <tr>
                        <td colspan="3">
                            <br />
                            <telerik:RadGrid ID="itemsList" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                                Skin="Simple" Visible="False">
                                <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="codigo" HeaderText="" UniqueName="codigo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="descripcion" HeaderText="" UniqueName="descripcion">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="unidad" HeaderText="" UniqueName="unidad">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="cantidad" HeaderText="" UniqueName="cantidad">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="precio" HeaderText="" UniqueName="precio" DataFormatString="{0:C}" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="importe" HeaderText="" UniqueName="importe" DataFormatString="{0:C}" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                    CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" CausesValidation="False" />
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
                        <td colspan="3"><br /></td>
                    </tr>
                </table>

            </fieldset>

        </asp:Panel>

        <br />

        <asp:Panel ID="panelResume" runat="server" Visible="False">

            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblResume" runat="server" Font-Bold="true" CssClass="titulos"></asp:Label>
                </legend>

                <br />

                <table width="100%" align="left">
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblSubTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblSubTotalValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblIVA" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <asp:panel ID="panelRetencion" runat="server" Visible="false">
                        <tr>
                            <td width="16%" align="left" style="width: 32%">
                                <asp:Label ID="lblRet" runat="server" CssClass="item" Font-Bold="True" Text="Retención 4%="></asp:Label>
                                &nbsp;<asp:Label ID="lblRetValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                    </asp:panel>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="left" width="16%">
                            <br /><br />
                            <asp:Button ID="btnCreateInvoice" runat="server" CausesValidation="False" 
                                CssClass="item" />
                                
                            <br /><br />
                        </td>
                    </tr>
                </table>

            </fieldset>

        </asp:Panel>
    
<%--  </telerik:RadAjaxPanel>--%>
  
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>

