function validate() {
    var taskName = document.getElementById("TaskName").value.trim();
  
    if (taskName === "") {
      alert("Please enter a valid card name.");
      return false;
    }
    return true;
  }