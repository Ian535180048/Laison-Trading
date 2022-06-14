<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JurnalPaging.aspx.cs" Inherits="SkripsiIanKeefe.JurnalPaging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Jurnal</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Jurnal</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Jurnal Bulan</li>
                        <asp:TextBox runat="server" ID="txtboxjurnalbulan" TextMode="Number" />
                        <asp:RangeValidator ErrorMessage="Range Bulan hanya dari bulan Januari hingga bulan Desember" MaximumValue="12" MinimumValue ="1" Type="Integer" ControlToValidate="txtboxjurnalbulan" runat="server" ForeColor="Red" />
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxjurnalbulan" runat="server" ForeColor="Red" />

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Jurnal Tahun</li>
                        <asp:TextBox runat="server" ID="txtboxtahun" TextMode="Number" />
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtboxtahun" runat="server" ForeColor="Red" />
                    </div>
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
            <asp:GridView runat="server" ID="gvjurnal" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped table-responsive">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Tanggal" ID="ltltanggal" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_tanggal" Text='<%# Eval("TANGGAL", "{0: dd MMM yyyy}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Keterangan" ID="ltlketerangan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_keterangan" Text='<%# Eval("DESCR") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="COA" ID="ltlcoa" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_coa" Text='<%# Eval("COA_NO") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Debit" ID="ltldebit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_debit" Text='<%# Eval("IN_AMT", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbl_total_debit" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Kredit" ID="ltlkredit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_kredit" Text='<%# Eval("OUT_AMT", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbl_total_kredit" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <br />
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Debit</li>
                        <asp:Label ID="lbltotaldebit" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Total Kredit</li>
                        <asp:Label ID="lbltotalkredit" runat="server" />
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <br />

        <asp:UpdatePanel runat="server" ID="upneraca" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label Text="Hasil Neraca Saldo" runat="server" />
            <asp:GridView runat="server" ID="gvneraca" AutoGenerateColumns="false" GridLines="None" CssClass="table table-hover table-striped table-responsive">
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="COA" ID="ltlcoa" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_coa" Text='<%# Eval("COA_NO") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Keterangan" ID="ltlketerangan" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_keterangan" Text='<%# Eval("DESCR") %>' runat="server" />
                            <%--<asp:Label ID="lbl_trans_no" Text='<%# Eval("TRANS_NO") %>' runat="server"   />--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Debit" ID="ltldebit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_debit" Text='<%# Eval("SUM_IN_AMT", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Literal Text="Kredit" ID="ltlkredit" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_kredit" Text='<%# Eval("SUM_OUT_AMT", "{0:Rp 0,00}") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <br />
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Total Debit</li>
                        <asp:Label ID="lbl_debit_neraca" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Total Kredit</li>
                        <asp:Label ID="lbl_kredit_neraca" runat="server" />
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
