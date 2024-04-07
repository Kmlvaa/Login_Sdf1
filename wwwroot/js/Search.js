const searchInput = document.querySelector(".search-input")
const searchResult = document.querySelector(".search-result")
let cachedSearchValue;

searchInput.addEventListener('keyup', (e) => {
    console.log("Hello")
    const value = e.target.value.trim();
    if (value.length < 3) {
        cachedSearchValue = null
        searchResult.innerHTML = ''
        return
    }
    if (value == cachedSearchValue) return
    cachedSearchValue = value
    renderSearchResult(value)
})

function renderSearchResult(searchValue) {
    fetch(`http://localhost:7172/shop/search?input=${searchValue}`)
        .then(x => x.text())
        .then(x => {
            searchResult.innerHTML = ''
            searchResult.innerHTML = x
        })
}