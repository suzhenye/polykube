package main

import (
	"fmt"
	"net/http"
)

const response = `{ "msg": "hello world" }`

func example(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Access-Control-Allow-Origin", "*")
	w.Header()["Content-Type"] = []string{"application/json"}
	fmt.Fprint(w, response)
}

func main() {
	http.HandleFunc("/", example)
	http.ListenAndServe(":80", nil)
}
