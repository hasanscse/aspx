<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="BillFiltering.aspx.cs" Inherits="EDU.UI.Admin.License.BillFiltering" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function ConfirmDelete() {
        if (confirm("Are you sure to want to Delete?") == true)
            return true;
        else
            return false;
    }  
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

<div class="row">
        <div class="col-sm-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <!-- panel-btns -->                   
                    <h3 class="panel-title">
                    Bill Filtering
                    </h3>
                </div>
                <div class="panel-body">           

                       <div id="lblgroupClient" runat="server" class="form-group">
                        <label class="col-sm-4 control-label">
                           Category :</label>
                            <div class="col-sm-8">
                             <asp:DropDownList ID="cmbCategory"  CssClass="form-control" runat="server">
                            </asp:DropDownList> 
                            <asp:CheckBox ID="chkAllCategory" Checked="true" Text="All" runat="server" />                          
                                               
                        </div>                       
                    </div>                    
                      
                            <div id="Div2" runat="server" class="form-group">
                        <label class="col-sm-4 control-label">
                           Sub Category :</label>
                            <div class="col-sm-8">
                             <asp:DropDownList ID="cmbSubCategory" CssClass="form-control" runat="server">
                            </asp:DropDownList> 
                             <asp:CheckBox ID="chkAllSubCategory" Checked="true" Text="All" runat="server" />                          
                                               
                        </div>                       
                    </div>           
                                
                       <div id="Div3" runat="server" class="form-group">
                        <label class="col-sm-4 control-label">
                            City:</label>
                            <div class="col-sm-8">
                             <asp:DropDownList ID="cmbCity" AutoPostBack="true" CssClass="form-control" 
                                    runat="server" onselectedindexchanged="cmbCity_SelectedIndexChanged">
                            </asp:DropDownList>  
                             <asp:CheckBox ID="chkAllCity" Checked="true" Text="All" runat="server" />                          
                                               
                        </div>                       
                    </div> 

                    <div id="Div5" runat="server" class="form-group">
                        <label class="col-sm-4 control-label">
                            Thana:</label>
                            <div class="col-sm-8">
                             <asp:DropDownList ID="cmbZone" CssClass="form-control" runat="server">
                            </asp:DropDownList>  
                             <asp:CheckBox ID="chkAllZone" Checked="true" Text="All" runat="server" />                          
                                               
                        </div>                       
                    </div>  
                    <div class="form-group">
                        <label class="col-sm-4 control-label">
                          For The Month :</label>
                         <div class="col-sm-8">
                            <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="January">January</asp:ListItem>
                                <asp:ListItem Value="February">February</asp:ListItem>
                                <asp:ListItem Value="March">March</asp:ListItem>
                                <asp:ListItem Value="April">April</asp:ListItem>
                                <asp:ListItem Value="May">May</asp:ListItem>
                                <asp:ListItem Value="June">June</asp:ListItem>
                                <asp:ListItem Value="July">July</asp:ListItem>
                                <asp:ListItem Value="August">August</asp:ListItem>
                                <asp:ListItem Value="September">September</asp:ListItem>
                                <asp:ListItem Value="October">October</asp:ListItem>
                                <asp:ListItem Value="November">November</asp:ListItem>
                                <asp:ListItem Value="December">December</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox ID="chkAllDropDownList1" Checked="true" Text="All" runat="server" />  
                        </div>
                    </div>
                   <div class="form-group">
                        <label class="col-sm-4 control-label">
                         Bill Paid Status :</label>
                         <div class="col-sm-8">
                            <asp:DropDownList ID="cmbBillStatus" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Paid</asp:ListItem>
                                <asp:ListItem Value="0">Unpaid</asp:ListItem>
                                
                            </asp:DropDownList>
                            <asp:CheckBox ID="chkAllbillStatus" Checked="true" Text="All" runat="server" />  
                        </div>
                    </div>

                     <div class="form-group">
                        <label class="col-sm-4 control-label">
                         Active Status :</label>
                         <div class="col-sm-8">
                            <asp:DropDownList ID="cmbActiveStatus" CssClass="form-control" runat="server">
                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                
                            </asp:DropDownList>
                            <asp:CheckBox ID="chKAllas" Checked="true" Text="All" runat="server" />  
                        </div>
                    </div>
                       
                </div>
                <div class="panel-footer">                
                    <div class="row" style="text-align: right; padding-right: 30px;">
                       <asp:Button class="btn btn-primary" ID="btnOutSideDhakaPrint" runat="server" 
                            Text="Out Side Dhaka Print" onclick="btnOutSideDhakaPrint_Click"  />
                      <asp:Button class="btn btn-primary" ID="btnPrint" runat="server" 
                            Text="Print" onclick="btnPrintALL_Click"  />
                        <asp:Button class="btn btn-success" ID="btnView" runat="server" 
                            Text="View" onclick="btnView_Click"  />

                    </div>
                </div>             


            </div>
            <!-- panel -->
        </div>
        <!-- col-sm-6 -->

           <div runat="server" visible="true" id="div1" class="col-sm-16">
        <div class="panel panel-primary">
            <div class="panel-body">
                <div class="panel-btns">
                    <a class="panel-close" href="#">×</a> <a class="minimize" href="#">−</a>
                </div>
                <asp:GridView ID="dgbillfiltering" runat="server" AutoGenerateColumns="false" Width="1300px"   lowPaging="true" BackColor="OrangeRed" ForeColor="Snow" BorderColor="Orange" Font-Names="Comic Sans MS">
                <HeaderStyle BackColor="DeepPink" Font-Italic="false"  Height="50" ForeColor="Snow"  />
                 <PagerStyle BackColor="IndianRed" ForeColor="PeachPuff" Font-Size="Large" HorizontalAlign="Right"/>
                  
                      <Columns>
                        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id" ItemStyle-Width="100"   />
                    </Columns>
                      <Columns>
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Shop Name" ItemStyle-Width="100"   />
                    </Columns>
                      <Columns>
                        <asp:BoundField DataField="OWNER_NAME" HeaderText="Owner Name" ItemStyle-Width="100"   />
                    </Columns>
                    <Columns>
                     <asp:BoundField DataField="ADDRESS" HeaderText="Address" ItemStyle-Width="100" />
                    </Columns>
                     <Columns>
                      <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number" ItemStyle-Width="50"/>
                     </Columns>                          
                      <Columns>
                       <asp:BoundField DataField="EMAIL" HeaderText="Email" ItemStyle-Width="80" />
                      </Columns>                       
                        <Columns>
                        <asp:BoundField DataField="CATEGORY_NAME" HeaderText="CATEGORY NAME" ItemStyle-Width="50" />
                         </Columns>
                         <Columns>
                         <asp:BoundField DataField="SUBCATEGORY_NAME" HeaderText="SUBCATEGORY NAME" ItemStyle-Width="50" />
                         </Columns>
                         <Columns>
                       <asp:BoundField DataField="ZONE_NAME" HeaderText="THANA NAME" ItemStyle-Width="70" />
                        </Columns>
                         <Columns>
                         <asp:BoundField DataField="MY_CHARGE" HeaderText="M/Y CHARGE" ItemStyle-Width="70" />
                          </Columns>                     
                                                  


                      <Columns>
                        <asp:BoundField DataField="BILL_NO" HeaderText="BILL NO" ItemStyle-Width="150"   />
                    </Columns>                
                       <Columns>
                        <asp:BoundField DataField="A_STATUS" HeaderText="Active Status" ItemStyle-Width="150"   />
                    </Columns>           
                    
                    <Columns>
                        <asp:BoundField DataField="STATUS" HeaderText="BILL PAID STATUS" ItemStyle-Width="50" />
                    </Columns>                   
                     <Columns >
                                <asp:TemplateField>
                                    <ItemTemplate >
                                        <asp:Label Visible="false" ID="lblBILL_NO" runat="server" Text='<%# Bind("BILL_NO") %>'></asp:Label>  
                                         <asp:Button BackColor="green" ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" STYLE="color:White;"
                                            Text="Bill Confirm" ShowSelectButton="True"></asp:Button>                                                                             
                                        <asp:Button BackColor="Red" ID="btndelete" OnClick="btndelete_Click" runat="server"  OnClientClick="return ConfirmDelete()" STYLE="color:White;"
                                            Text="Delete" ShowSelectButton="True"></asp:Button>
                                            <asp:Button BackColor="blue" ID="btnPrint" OnClick="btnPrint_Click" runat="server" STYLE="color:White;"
                                            Text="Print" ShowSelectButton="True"></asp:Button>
                                                
                                           
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>
    </div>   

     <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
