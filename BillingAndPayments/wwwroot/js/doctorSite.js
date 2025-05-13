// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

@section Scripts {

    @{ await Html.RenderPartialAsync("_ValidationScriptsPartial"); }
    <script>

        // Add Availability Slot

        document.getElementById("add-slot").addEventListener("click", function () {

            const container = document.getElementById("availability-container");

        const index = container.querySelectorAll(".availability-group").length;

        const html = `
        <div class="availability-group mb-3">
            <div class="form-row">
                <div class="col">
                    <input name="Availabilities[${index}].Day" class="form-control" placeholder="Day (e.g., Monday)" />
                </div>
                <div class="col">
                    <input name="Availabilities[${index}].TimeSlot" class="form-control" placeholder="Time Slot (e.g., 10 AM - 12 PM)" />
                </div>
                <div class="col-auto">
                    <button type="button" class="btn btn-danger remove-slot">Remove</button>
                </div>
            </div>
        </div>

        `;

        container.insertAdjacentHTML("beforeend", html);

        });

        // Remove Availability Slot

        document.addEventListener("click", function (e) {

            if (e.target.classList.contains("remove-slot")) {

            e.target.closest(".availability-group").remove();

            }

        });
    </script>

}
