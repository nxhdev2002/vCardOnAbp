import http from 'k6/http';
import { check, sleep } from 'k6';
import { SharedArray } from 'k6/data';

export const options = {
  stages: [
    { duration: '1s', target: 5 }, // ramp-up đến 50 CCU trong 1 phút
    { duration: '5s', target: 10 }, // giữ 200 CCU trong 3 phút
    { duration: '2m', target: 0 }, // ramp-down về 0 CCU trong 1 phút
  ],
};

const baseUrl = 'https://localhost:44396/api/app/cards';
const token = 'Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkNBQjVCQzU1MzdGREE4QzBCOTIzRjQzRTFBQTEzQTMxMEY1QTNEQkYiLCJ4NXQiOiJ5clc4VlRmOXFNQzVJX1EtR3FFNk1ROWFQYjgiLCJ0eXAiOiJhdCtqd3QifQ.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM5Ni8iLCJleHAiOjE3MzE3MzQ0NjUsImlhdCI6MTczMTczMDg2NSwiYXVkIjoiVkNhcmRPbkFicCIsInNjb3BlIjoiVkNhcmRPbkFicCIsImp0aSI6ImY2MDI2Njc1LTE0N2UtNDcwZC05NTI4LWExODIxYjQxODUyYiIsInN1YiI6IjU1YTU4MDFjLWVmOGItNzViNS03NDNmLTNhMTQ5NDYxYmMwZSIsInVuaXF1ZV9uYW1lIjoiYWRtaW4iLCJvaV9wcnN0IjoiVkNhcmRPbkFicF9Td2FnZ2VyIiwib2lfYXVfaWQiOiI5NjMwOTczMy01NDZmLTk1NWQtYjVlMy0zYTE1MDYyZGExMjEiLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiIsImdpdmVuX25hbWUiOiJhZG1pbiIsInJvbGUiOiJhZG1pbiIsImVtYWlsIjoiYWRtaW5AYWJwLmlvIiwiZW1haWxfdmVyaWZpZWQiOiJGYWxzZSIsInBob25lX251bWJlcl92ZXJpZmllZCI6IkZhbHNlIiwiY2xpZW50X2lkIjoiVkNhcmRPbkFicF9Td2FnZ2VyIiwib2lfdGtuX2lkIjoiMjFhY2EzNTgtMmE5Ni1hOTJhLTdmZjctM2ExNjQ1NjBkNTIzIn0.EEIGjm5VDTMjGtQH1xvtMNXuqLLrSJEmAxvzgkpEwpiS5PwNZkZojS4bmU-B6hewnQ2cjxJppnMC-Jn7-qhTYcw1QIZ0-FTVDWKJNurQypkA3M2MEtLrOfItRtH3ogZfQ0iPUtaKyVKpLI3L9oen36LPy5vqKXiBdhgN5-sAkcmY5nPcRXZUdMo4r1IDJvDcz9VutdmM1OVPRS4ZSPZj3ActkBOg8cycN1XWC5ekhE_BUiXTSTJX8LDldBlPeaguf0jSYRlpv8mS4Et1vC6vaaQtavdg_pBUYyztDla8Q1WJn8yLOwjPuO2GkBQDiPLl9eOv9wfkJtRYNXqfashS2A'; // Thay <YOUR_TOKEN> bằng token thật.

const payload = JSON.stringify({
  amount: 2.5,
  cardName: '<script>alert(123)</script>',
  binId: '7c32ba03-08e6-fd65-050c-3a1645235a68',
});

const params = {
  headers: {
    'Content-Type': 'application/json',
    Authorization: token,
  },
};

export default function () {
  const res = http.post(baseUrl, payload, params);

  check(res, {
    'is status 200': (r) => r.status === 200,
    'is response valid': (r) => r.json().hasOwnProperty('success'),
  });

  sleep(1); // Nghỉ 1 giây trước khi thực hiện request tiếp theo
}
