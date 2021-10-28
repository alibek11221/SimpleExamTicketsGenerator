// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
let questionsCounter = 1;
// Write your JavaScript code.
$('#add-question').click(function () {
    questionsCounter++;
    const html = `    <tr data-number="${questionsCounter}" class="questions">
        <th scope="row">${questionsCounter}</th>
        <th><textarea style="width: 100%;"></textarea></th>
    </tr>`
    $('#quest-table > tbody').append(html)
})

function generateData() {
    const GroupName = $('#group-name').val();
    const DisciplineName = $('#discipline-name').val();
    const Semester = $('#semester-number').val();
    const Maximum = $('#tickets-max').val();
    const Questions = [];
    $('.questions').map(function (index) {
        const questionsText = $(this).find('textarea').val();
        Questions.push({
            Number: parseInt($(this).data('number')),
            Questions: questionsText
        })
    })
    return {
        "GroupName": GroupName,
        "DisciplineName": DisciplineName,
        "Semester": Semester,
        "Questions": Questions,
        Maximum
    }
}
