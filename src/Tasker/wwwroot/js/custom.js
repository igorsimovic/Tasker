Array.prototype.firstOrDefault = function (propName, propVal) {
    var result = null;
    result = this.filter(function (item) {
        return item[propName] === propVal;
    })[0];
    return result;
};