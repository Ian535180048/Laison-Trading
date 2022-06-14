<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BiayaPagingAdmin.aspx.cs" Inherits="SkripsiIanKeefe.BiayaPagingAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Biaya</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Daftar Biaya</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">No Biaya</li>
                        <asp:TextBox runat="server" ID="txtboxtransno" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Beban Tipe</li>
                        <asp:DropDownList runat="server" ID="ddlbebantipe">
                            <asp:ListItem Text="Beban Listrik" Value="BL"></asp:ListItem>
                            <asp:ListItem Text="Beban Internet" Value="BI"></asp:ListItem>
                            <asp:ListItem Text="Beban Air" Value="BA"></asp:ListItem>
                            <asp:ListItem Text="Beban Pajak" Value="BP"></asp:ListItem>
                            <asp:ListItem Text="Beban Administrasi" Value="BAD"></asp:ListItem>
                            <asp:ListItem Text="Beban Lain-lain" Value="BDLL"></asp:ListItem>
                            <asp:ListItem Text="Semua" Value=""></asp:ListItem>
                        </asp:DropDownList>
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

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Search" CssClass="btn btn-primary" runat="server" ID="ltlsearch" OnClick="ltlsearch_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Add" CssClass="btn btn-primary" runat="server" ID="ltladd" OnClick="ltladd_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <br />
    <asp:UpdatePanel runat="server" ID="upcust" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvbeban" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvbeban_RowCommand">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tanggal Pembayaran" ID="ltltanggal" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_tanggal" Text='<%# Eval("TANGGAL", "{0: MM/dd/yyyy}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nomor Transaksi" ID="ltltransno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server" CommandName="DTL" CausesValidation="false" />
                            <asp:Label ID="lb_trans_id" Text='<%# Eval("TRANS_BIAYA_ID") %>' runat="server" Visible="false" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tipe Beban" ID="ltlbebantipe" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_beban_tipe" Text='<%# Eval("DESCR") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Total" ID="ltlammount" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_ammount" Text='<%# Eval("AMT","{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Edit" ID="ltledit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgedit" ImageUrl="~/Image/edit.png" CommandName="ED" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Delete" ID="ltledit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgedelete" ImageUrl="~/Image/trash.png" CommandName="DEL" runat="server" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
