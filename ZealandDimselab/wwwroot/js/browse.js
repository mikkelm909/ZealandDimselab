$(document).on("click", ".browse", function () {
    var file = $(this).parents().find(".file");
    file.trigger("click");
});
$('input[type="file"]').change(function (e) {
    var fileName = e.target.files[0].name;
    $("#file").val(fileName);

    var reader = new FileReader();
    reader.onload = function (e) {
        // get loaded data and render thumbnail.
        document.getElementById("preview").src = e.target.result;
    };
    // read the image file as a data URL.
    reader.readAsDataURL(this.files[0]);
});

// Code source: https://learncodeweb.com/snippets/browse-button-in-bootstrap-4-with-select-image-preview/

//<div class="form-group">
//    <label>Image</label>
//    <!--source: https://learncodeweb.com/snippets/browse-button-in-bootstrap-4-with-select-image-preview/ -->
//<input asp-for="FileUpload" type="file" name="img[]" class="file" style="visibility: hidden; position: absolute;" accept="image/*">
//    <div class="input-group my-3">
//    <input type="text" class="form-control" disabled placeholder="Upload File" id="file">
//    <div class="input-group-append">
//    <button type="button" class="browse btn btn-primary">Browse...</button>
//    </div>
//    </div>
//    <div class="w-100">
//    <img src="https://placehold.it/80x80" id="preview" class="img-thumbnail">
//    </div>
//    </div>