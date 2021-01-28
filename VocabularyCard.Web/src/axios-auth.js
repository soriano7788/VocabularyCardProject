import axios from 'axios'
import store from "./store/store.js";

const instance = axios.create({
    baseURL: 'https://identitytoolkit.googleapis.com/v1/'
})

instance.defaults.baseURL = 'https://127.0.0.1/vocabulary/api/account';
instance.defaults.headers.get["Accepts"] = "application/json";
instance.defaults.headers.common['Access-Control-Allow-Origin'] = '*';


instance.interceptors.request.use(config => {
    store.dispatch("setLoadingSpinnerVisibility", true);
    return config;
})
instance.interceptors.response.use(function (res) {
    store.dispatch("setLoadingSpinnerVisibility", false);
    return res;
}, function (error) {
    store.dispatch("setLoadingSpinnerVisibility", false);
    alert("Sigh in fail");
});

export default instance;