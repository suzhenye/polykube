package main

/*
char* foo(void) { return "hello, world!"; }
*/
import "C"

import (
	"fmt"
	"net/http"

	unidecode "gopkgs.com/unidecode.v1"
)

func exampleWrapper(s string) func(http.ResponseWriter, *http.Request) {
	return func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Access-Control-Allow-Origin", "*")
		w.Header()["Content-Type"] = []string{"application/json"}
		fmt.Fprint(w, s)
	}
}

func main() {
	response1 := C.GoString(C.foo())
	response2 := unidecode.Unidecode(response1)

	http.HandleFunc("/alt", exampleWrapper(response1))
	http.HandleFunc("/", exampleWrapper(response2))
	http.ListenAndServe(":80", nil)
}
