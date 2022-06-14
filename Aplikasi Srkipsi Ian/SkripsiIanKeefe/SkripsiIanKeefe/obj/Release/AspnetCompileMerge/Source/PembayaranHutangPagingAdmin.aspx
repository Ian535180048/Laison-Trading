<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PembayaranHutangPagingAdmin.aspx.cs" Inherits="SkripsiIanKeefe.PembayaranHutangPagingAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pembayaran Hutang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Pembayaran Hutang Paging</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Transaksi No Pembayaran Hutang</li>
                        <asp:TextBox runat="server" ID="txtboxtransnopmbyrn" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Transaksi No Hutang</li>
                        <asp:TextBox runat="server" ID="txtboxtransnohutang" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Tanggal Mulai</li>

                    <asp:TextBox runat="server" ID="txtboxtanggalawal" TextMode="Date" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="txtboxtanggalawal" ForeColor="Red" runat="server" />
                </div>
                <div class="col-md-6">
                    <li class="breadcrumb-item active">Tanggal Akhir</li>

                    <asp:TextBox runat="server" ID="txtboxtanggalakhir" TextMode="Date" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="txtboxtanggalakhir" ForeColor="Red" runat="server" />
                </div>
            </div>

        </div>
    </div>
    <br />
    <div class="justify-content-end">
        <table>
            <tr>
                <td style="width: 100%">
                    <asp:LinkButton Text="Search" CssClass="btn btn-primary" runat="server" ID="ltlsearch" OnClick="ltlsearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:UpdatePanel runat="server" ID="upcust" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvhutang" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvhutang_RowCommand">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tanggal Transaksi" ID="ltltanggal" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_tanggal" Text='<%# Eval("TANGGAL", "{0: dd MMM yyyy}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Transaksi Pembayaran Hutang No" ID="ltltransnopmbyrab" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_trans_no_pmbyrn" Text='<%# Eval("TRANS_NO") %>' runat="server" CausesValidation="false" CommandName="DTL" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Transaksi Hutang No" ID="ltltransno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_trans_no" Text='<%# Eval("TRANS_NO_HUTANG") %>' runat="server" CausesValidation="false" CommandName="DTL_HUTANG" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Pembayaran Total" ID="ltltotal" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_trans_id" Text='<%# Eval("TRANS_BYR_UTANG_ID") %>' runat="server" Visible="false"/>
                            <asp:Label ID="lbl_pembayaran" Text='<%# Eval("TOTAL", "{0:0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Edit" ID="ltledit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Image/edit.png" CommandName="ED" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Delete" ID="ltldelete" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/Image/trash.png" CommandName="DEL" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
