window.mealventoryAuth = {
    saveUser: function (userId, username) {
        localStorage.setItem("mealventory_userId", userId);
        localStorage.setItem("mealventory_username", username);
    },
    getUser: function () {
        return {
            userId: localStorage.getItem("mealventory_userId"),
            username: localStorage.getItem("mealventory_username")
        };
    },
    clearUser: function () {
        localStorage.removeItem("mealventory_userId");
        localStorage.removeItem("mealventory_username");
    }
};