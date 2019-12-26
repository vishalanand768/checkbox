<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkboxprac.aspx.cs" Inherits="checkbox.checkboxprac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Name :</td>
                    <td><asp:TextBox ID="txtname" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Age :</td>
                    <td><asp:TextBox ID="txtage" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>Country :</td>
                    <td><asp:DropDownList ID="ddlcountry" runat="server"></asp:DropDownList></td>
                </tr>

                  <tr>
                    <td>Hobbies  :</td>
                    <td><asp:CheckBoxList ID="chkhobbies" runat="server" RepeatColumns="3">
                        <asp:ListItem Text="Cricket" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Music" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Movies" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Cooking" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Swimming" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Coding" Value="6"></asp:ListItem>
                        </asp:CheckBoxList></td>
                </tr>

                <tr>
                    <td>I agree  :</td>
                    <td><asp:CheckBox ID="chkiagree" runat="server" /></td>
                </tr>

                <tr>
                    <td></td>
                    <td><asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" /></td>
                </tr>

                <tr>
                    <td></td>
                    <td><asp:GridView ID="grd" runat="server" OnRowCommand="grd_RowCommand" AutoGenerateColumns="true">
                        <Columns>

                             <asp:TemplateField HeaderText="Hobbies">
                            <ItemTemplate>
                                <%#Eval("Hobbies") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="I Agree ?">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnstatus" runat="server" Text='<%#Eval("Iagree").ToString()=="1" ? "Yes" : "No" %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:Button ID="btndelete" runat="server" Text="Delete" CommandName="A" CommandArgument='<%#Eval("id") %>' />
                           <asp:Button ID="btnedit" runat="server" Text="Edit" CommandName="B" CommandArgument='<%#Eval("id") %>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        </asp:GridView></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
