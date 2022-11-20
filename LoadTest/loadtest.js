import http from 'k6/http';

export const options = {
  scenarios: {
    constant_request_rate: {
      executor: 'constant-arrival-rate',
      rate: 105000,
      timeUnit: '1s', // 1000 iterations per second, i.e. 1000 RPS
      duration: '60s',
      preAllocatedVUs: 400, // how large the initial pool of VUs would be
      maxVUs: 600, // if the preAllocatedVUs are not enough, we can initialize more
    },
  },
};

export default function () {
    const url = 'http://localhost:5000/Basket/api/Basket/Save';
    const payload = JSON.stringify({
        "data": [
            {
              "basketId": "1",
              "customerId": 1,
              "productName": "Etek",
              "createdAt": "2022-11-19T20:50:09.856Z",
              "quantity": 10,
              "price": 10
            },
            {
              "basketId": "1",
              "customerId": 1,
              "productName": "Jile",
              "createdAt": "2022-11-19T20:50:09.856Z",
              "quantity": 1,
              "price": 120
            },
            {
              "basketId": "1",
              "customerId": 1,
              "productName": "Şapka",
              "createdAt": "2022-11-19T20:50:09.856Z",
              "quantity": 2,
              "price": 3
            }
          ]
    });
  
    const params = {
      headers: {
        'Content-Type': 'application/json',
      },
    };
  
    http.post(url, payload, params);
  }