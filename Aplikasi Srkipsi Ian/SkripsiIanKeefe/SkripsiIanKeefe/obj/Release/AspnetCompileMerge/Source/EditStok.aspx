<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditStok.aspx.cs" Inherits="SkripsiIanKeefe.EditStok" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="smApplication" />

    <h1 class="mt-4">Pembelian Stok Barang</h1>

    <ol class="breadcrumb mb-4">
        <li class="breadcrumb-item active">Edit Transaction Pembelian Stok Barang</li>
    </ol>

    <br />
    <div class="container">
        <div class="col-lg-7">
            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Product Nama</li>
                        <asp:Label ID="lblprodnama" runat="server" />
                    </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-floating">
                            <li class="breadcrumb-item active">Trans No</li>
                            <asp:Label id="lbltransno" runat="server" />
                        </div>
                    </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Tanggal Transaksi</li>
                        <asp:TextBox ID="txtboxtanggal" TextMode="Date" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Required" ControlToValidate="txtboxtanggal" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <li class="breadcrumb-item active">Ammount</li>
                        <asp:TextBox runat="server" ID="txtboxammount" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ControlToValidate="txtboxammount" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Number Only" ControlToValidate="txtboxammount" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <li class="breadcrumb-item active">Harga Transaksi</li>
                        <asp:TextBox ID="txtboxharga"  runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="txtboxharga" runat="server" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="Number Only" ControlToValidate="txtboxharga" ValidationExpression="^\d+$" runat="server" ForeColor="Red" />
                    </div>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <div class="form-floating mb-3 mb-md-0">
                        <asp:LinkButton Text="Submit" CssClass="btn btn-primary" runat="server" ID="lbadd" OnClick="lbadd_Click"/>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-floating">
                        <asp:LinkButton Text="Cancel" CssClass="btn btn-primary" runat="server" ID="lbcancel" OnClick="lbcancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
