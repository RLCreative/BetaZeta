<%@ Page Title="" Language="C#" MasterPageFile="~/Internal/internal.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Viceroy.Internal._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MetaData" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div>

    <!-- ANONYMOUS -- SHOULD REDIRECT TO "../home" -->
    <div id="divAnonymous" runat="server" visible="true">
        Oops you have reached this page by accident.  Please continue to the homepage <a href="../home">here</a>!
    </div>
    <!-- ADMIN -->
    <div id="divAdmin" runat="server" visible="false">
        <ul class="default">
            <li><a href="/internal/admin/createuser.aspx">Create a User</a></li>
            <li><a href="/internal/admin/manageuser.aspx">Manage a User</a></li>
            <li><a href="/internal/admin/vieworders.aspx">View all orders</a></li>            
        </ul>
    </div>
    <!-- RETAILER -->
    <div id="divRetailer" runat="server" visible="false">
        <h1>Downloads</h1>
        <h2>Product Images</h2>
        <p>
            We use <strong>Flickr</strong>to host our downloadable images. Images are available
            in various sizes, to meet your needs:
        </p>
        <p>
            <span style="background-image: url(/images/flickr.png); background-position: top left;
                background-repeat: no-repeat; margin-right: 30px; height: 16px; display: block;">
                <a href="http://www.flickr.com/photos/regallager/collections/" style="padding-left: 20px;">
                    Go to the Regal Lager photostream</a></span>
        </p>
        <p>
            <strong>How to Use Flickr:</strong>
        </p>
        <ol class="default">
            <li>Once on Flickr, you will see teh "collections" of images broken up by brand</li>
            <li>Double-click on the collection containing images that you want</li>
            <li>Click on the Image set</li>
            <li>Click on the image thumbnail for a larger view</li>
            <li>Once in the larger view, click on Actions, then select "View All Sizes"</li>
            <li>Click on the link corresponding to the size of the image you want: Small, Medium
                500, Original, etc. (You may bypass a step by right clicking on the size link, and
                choosing &quot;Save target As...&quot;)</li>
            <li>After the image loads completely, right click on the image and choose "Save picture
                as..."</li>
        </ol>
        <p>
            <strong>You, or someone else may wish to link to this image from a website or blog.
                If so:</strong>
        </p>
        <ol class="default">
            <li>Once on Flickr, you will see several "collections" of images broken up by brand
            </li>
            <li>Double-click on the collection containing images that you want</li>
            <li>Click on the Image set</li>
            <li>Click on the image thumbnail for a larger view</li>
            <li>Once in the larger view, click on Share, then select "Grab the Link"</li>
            <li>Copy the link and use it as the image's source.</li>
        </ol>
        <p>
            <strong>If you have questions, or need a print ready CMYK file?</strong>
            <br />
            Please contact Allen Bourne at 678-819-5803, or via <a href="mailto:allen@regallager.com">
                email</a>. Be sure to include dimensions, resolution and any other information
            you may have about the destination of the file. Thanks.
        </p>
        <hr />
        <h2>Order Forms</h2>
        <ul class="default">
            <li>United States:</li>
            <li><a href="/downloads/rl order forms.pdf" title="Order Form" target="_blank">Latest
                Regal Lager Order Form (Print/Fax)</a></li>
            <li><a href="/downloads/RL Order Form - US.pdf" title="Order Form" target="_blank">Latest
                Regal Lager Order Form (Dynamic PDF)</a></li>
            <li>Canada:</li>
            <li><a href="/downloads/rl canadian order forms.pdf" title="Order Form" target="_blank">
                Latest Regal Lager Order Form (Print/Fax)</a></li>
            <li><a href="/downloads/RL Order Form - Canada.pdf" title="Order Form" target="_blank">
                Latest Regal Lager Order Form (Dynamic PDF)</a></li>
        </ul>
        <hr />
        <h2>Brand Logos</h2>
        <ul class="default">
            <li>Bambino Mio</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>Cybex</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>Dekor</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>Lascal</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>My Carry Potty</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
        </ul>
        <hr />
        <h2>Web Banners</h2>
    </div>
    <!-- SALESREP -->
    <div id="divSalesrep" runat="server" visible="false">
        <h1>Downloads</h1>
        <h2>Product Images</h2>
        <p>
            We use <strong>Flickr</strong>to host our downloadable images. Images are available
            in various sizes, to meet your needs:
        </p>
        <p>
            <span style="background-image: url(/images/flickr.png); background-position: top left;
                background-repeat: no-repeat; margin-right: 30px; height: 16px; display: block;">
                <a href="http://www.flickr.com/photos/regallager/collections/" style="padding-left: 20px;">
                    Go to the Regal Lager photostream</a></span>
        </p>
        <p>
            <strong>How to Use Flickr:</strong>
        </p>
        <ol class="default">
            <li>Once on Flickr, you will see teh "collections" of images broken up by brand</li>
            <li>Double-click on the collection containing images that you want</li>
            <li>Click on the Image set</li>
            <li>Click on the image thumbnail for a larger view</li>
            <li>Once in the larger view, click on Actions, then select "View All Sizes"</li>
            <li>Click on the link corresponding to the size of the image you want: Small, Medium
                500, Original, etc. (You may bypass a step by right clicking on the size link, and
                choosing &quot;Save target As...&quot;)</li>
            <li>After the image loads completely, right click on the image and choose "Save picture
                as..."</li>
        </ol>
        <p>
            <strong>You, or someone else may wish to link to this image from a website or blog.
                If so:</strong>
        </p>
        <ol class="default">
            <li>Once on Flickr, you will see several "collections" of images broken up by brand
            </li>
            <li>Double-click on the collection containing images that you want</li>
            <li>Click on the Image set</li>
            <li>Click on the image thumbnail for a larger view</li>
            <li>Once in the larger view, click on Share, then select "Grab the Link"</li>
            <li>Copy the link and use it as the image's source.</li>
        </ol>
        <p>
            <strong>If you have questions, or need a print ready CMYK file?</strong>
            <br />
            Please contact Allen Bourne at 678-819-5803, or via <a href="mailto:allen@regallager.com">
                email</a>. Be sure to include dimensions, resolution and any other information
            you may have about the destination of the file. Thanks.
        </p>
        <hr />
        <h2>Order Forms</h2>
        <ul class="default">
            <li>United States:</li>
            <li><a href="/downloads/rl order forms.pdf" title="Order Form" target="_blank">Latest
                Regal Lager Order Form (Print/Fax)</a></li>
            <li><a href="/downloads/RL Order Form - US.pdf" title="Order Form" target="_blank">Latest
                Regal Lager Order Form (Dynamic PDF)</a></li>
            <li>Canada:</li>
            <li><a href="/downloads/rl canadian order forms.pdf" title="Order Form" target="_blank">Latest
                Regal Lager Order Form (Print/Fax)</a></li>
            <li><a href="/downloads/RL Order Form - Canada.pdf" title="Order Form" target="_blank">Latest
                Regal Lager Order Form (Dynamic PDF)</a></li>
        </ul>
        <hr />
        <h2>Brand Logos</h2>
        <ul class="default">
            <li>Bambino Mio</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>Cybex</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>Dekor</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>Lascal</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
            <li>My Carry Potty</li>
            <li><a href="/downloads/akord-logo-eps.zip">CMYK Ready .eps file</a></li>
            <li><a href="/downloads/akord-logo-jpg.zip">Web Ready .jpg file</a></li>
        </ul>
        <hr />
        <h2>Web Banners</h2>
    </div>

</div>


</asp:Content>
