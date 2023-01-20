// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var taskCounter = 0;

function addTask() {
    var taskTemplate = document.getElementById("task-template").innerHTML;
    var tasksContainer = document.getElementById("tasks-container");

    taskTemplate = taskTemplate.replace(/#index#/g, taskCounter);
    tasksContainer.insertAdjacentHTML("beforeend", taskTemplate);
    taskCounter++;
}

function removeTask(removeButton) {
    var taskContainer = removeButton.parentNode;
    taskContainer.parentNode.removeChild(taskContainer);
}

function updateTask(taskId) {
    var checkbox = document.getElementById("task-checkbox-" + taskId);
    var isCompleted = checkbox.checked;

    $.ajax({
        type: "POST",
        url: "/Projects/UpdateTask",
        data: { taskId: taskId, isCompleted: isCompleted },
        success: function (response) {
            console.log(response);
        },
        async: true
    });
}
