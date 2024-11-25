const url_server = `https://localhost:7189`
async function getToken() {
    const url_auth = `${url_server}/api/CW_ApiUser/auth`
    return await fetch(
        url_auth,
        {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify({
                Email: "orgeorgij@gmail.com",
                Password: "1234"
            })
        }
    ).then(response => {
        if(!response.ok)
            throw new Error('Fail to fetch JWT Token ...')
        return response.json()
    }).then(data => data.token)
}

async function loadProducts() {
    const url_products = `${url_server}/api/APIServiceProduct`
    const token = await getToken()
    return await fetch(
        url_products,
        {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        }
    ).then(response => {
        if(!response.ok)
            throw new Error('Fail to get products ...')
        return response.json()
    }).then(products => {
        let result = ''
        products.forEach(p => {
            result += 
            `
                <div class="card">
    <img src="./img/image.png" class="card-img-top" alt="Product Image">
    <div class="card-body">
        <h5 id="titleId" class="card-title">${p.name}</h5>
        <p id="descriptionId" class="card-text">${p.description}</p>
        <p id="priceId" class="card-text">${p.price}</p>
        <a href="#" class="btn">Buy</a>
    </div>
</div>
            `
        });
        document.getElementById("parent_products")
        .innerHTML = result
    })
}
loadProducts()
