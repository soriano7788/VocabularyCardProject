import axios from "axios";
import * as statusCode from "../consts/statusCode";

const state = {
    cardSets: []
};

const getters = {
    cardSets: state => {
        return state.cardSets;
    }
};

const mutations = {
    setCardSets: (state, payload) => {
        state.cardSets = payload;
    },
    appendNewCardSet: (state, newCardSet) => {
        state.cardSets.push(newCardSet);
    }
};

const actions = {
    fetchAllCardSets: ({ commit }) => {
        axios.get("cardset/getbyowner")
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    const cardSets = result.data;
                    commit("setCardSets", cardSets);
                }
            }).catch(error => {
                console.log(error);
            });
    },
    createCardSet: ({ commit }, cardSet) => {
        axios.post("cardset/Create", cardSet).then((res) => {
            const result = res.data;
            if (result.statusCode == statusCode.SUCCESS) {
                const newCardSet = result.data;
                commit("appendNewCardSet", newCardSet);
            }
        }).catch((error) => {
            console.log(error);
        });
    },
    clearCardSets: ({ commit }) => {
        commit("setCardSets", []);
    }
}

export default {
    state: state,
    getters: getters,
    mutations: mutations,
    actions: actions
};