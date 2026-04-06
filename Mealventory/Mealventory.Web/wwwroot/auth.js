// Owner 1: "Daniel Bajenov" has added 100% of the code in this file
// Principal Author: Daniel Bajenov
// Description: Small helper for storing and retrieving authenticated user info in localStorage.

window.mealventoryAuth = {
    /**
     * Save user info to localStorage.
     * @param {number} userId
     * @param {string} username
     */
    saveUser: function (userId, username) {
        localStorage.setItem("mealventory_userId", userId);
        localStorage.setItem("mealventory_username", username);
    },

    /**
     * Retrieve saved user info from localStorage.
     * @returns {{userId: string|null, username: string|null}}
     */
    getUser: function () {
        return {
            userId: localStorage.getItem("mealventory_userId"),
            username: localStorage.getItem("mealventory_username")
        };
    },

    /**
     * Clear saved user info from localStorage.
     */
    clearUser: function () {
        localStorage.removeItem("mealventory_userId");
        localStorage.removeItem("mealventory_username");
    }
};
