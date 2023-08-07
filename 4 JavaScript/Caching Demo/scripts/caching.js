// Creating a Cache
let cacheName = 'cacheDemo';

caches.open(cacheName).then(cache => {

});

// Adding an item to cache
// let url = '/index.html';
let url = 'https://cdn.pixabay.com/photo/2015/04/19/08/32/rose-729509_640.jpg';
caches.open(cacheName).then(cache => {
    cache.add(url).then(() => {
        console.log("Data cached 1");
    });
});

// adding more than one objects into cache memory with the help of array.
let urls = ['/index.html'];
caches.open(cacheName).then(cache => {
    cache.addAll(urls).then(() => {
        console.log("Data cached 2")
    });
});

// retrieving catch value
caches.open(cacheName).then(cache => {
    cache.match(url).then(settings => {
        console.log(settings);
    });
});

// retrieving all items in cache
// caches.open(cacheName).then((cache) => {
//     cache.keys().then((arrayOfRequest) => {
//         console.log(arrayOfRequest); // [Request,  Request]
//     });
// });

let urlToDelete = '/index.html';
caches.open(cacheName).then(cache => {
    cache.delete(urlToDelete);
});

caches.open('cacheDeleteDemo').then(cache => {
    cache.add(url).then(() => {
        console.log("Data cached 3");
    });
});

caches.delete('cacheDeleteDemo').then(() => {
    console.log('Cache successfully deleted!');
});