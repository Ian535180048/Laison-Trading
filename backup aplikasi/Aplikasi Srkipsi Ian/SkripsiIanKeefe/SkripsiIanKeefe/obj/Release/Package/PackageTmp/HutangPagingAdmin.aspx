<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HutangPagingAdmin.aspx.cs" Inherits="SkripsiIanKeefe.HutangPagingAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Hutang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Daftar Hutang</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Nomor Transaksi</li>
                        <asp:TextBox runat="server" ID="txtboxtransno" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nama Klien</li>
                        <asp:TextBox runat="server" ID="txtclientnama" />
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
                        <li class="breadcrumb-item active">Status</li>
                        <asp:DropDownList runat="server" ID="ddllunas">
                            <asp:ListItem Text="Lunas" Value="EXP"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="ACT"></asp:ListItem>
                            <asp:ListItem Text="Semua" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Nomor Klien</li>
                        <asp:TextBox runat="server" ID="txtboxclientno" />
                    </div>
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

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <br />
                    <asp:Label Text="Tidak Bisa Melakukan Action Tersebut Dikarenakan Pembayaran Dikarenakan Transaksi Telah Lunas" ID="lblerrorhutang" ForeColor="Red" Visible="false" runat="server" />
                    <br />
                    <asp:Label Text="Tidak Bisa Melakukan Action Tersebut Dikarenakan Hutang Tersebut Telah Dilakukan Pembayaran" ID="lblerrorpmbyrn" ForeColor="Red" Visible="false" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <br />
    <asp:UpdatePanel runat="server" ID="upcust" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView runat="server" ID="gvhutang" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped" OnRowCommand="gvhutang_RowCommand">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tanggal Hutang" ID="ltltanggal" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_trans_id" Text='<%# Eval("TRANS_HUTANG_ID") %>' runat="server" Visible="false" />
                            <asp:Label ID="lbl_tanggal" Text='<%# Eval("TANGGAL", "{0: MM/dd/yyyy}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nomor Transaksi" ID="ltltransno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' CausesValidation="false" CommandName="DTL" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nama Klien" ID="ltlclientnama" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_client_nama" Text='<%# Eval("CLIENT_NAME") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Nomor Klien" ID="ltlclientno" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lb_client_no" Text='<%# Eval("CLIENT_NO") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Total Hutang" ID="ltlhutangammount" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_hutang_ammount" Text='<%# Eval("HUTANG_AMT", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Sisa Hutang" ID="ltlsisahutang" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_sisa_hutang" Text='<%# Eval("SISA", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Status" ID="ltlisactive" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_is_active" Text='<%# Eval("IS_ACTIVE") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Pembayaran" ID="ltlpembayaran" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ImageUrl="~/Image/Pembayaran.png" CommandName="PMBYRN" runat="server" CausesValidation="false" />
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
