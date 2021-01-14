import axios from "axios";
import * as statusCode from "../consts/statusCode";
import router from "../../routes.js";

const state = {
    cards: []
};

const getters = {
    cards: state => {
        return state.cards;
    }
};

const mutations = {
    setCards: (state, payload) => {
        state.cards = payload;
    },
    appendNewCard: (state, card) => {
        state.cards.push(card);
    }
};

const actions = {
    fetchCardSetAllCards: ({ commit }, cardSetId) => {
        axios.get("cardset/GetCards/" + cardSetId)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    const cards = result.data;
                    commit("setCards", cards);
                }
            })
            .catch(error => {
                console.log(error);
            });
    },
    clearCards: ({ commit }) => {
        commit("setCards", []);
    },
    createCard: ({ commit }, payload) => {
        console.log(payload);
        axios.post("card/create/" + payload.cardSetId, payload.card)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    const newCard = result.data;
                    commit("appendNewCard", newCard);
                }
            })
            .catch(error => {
                console.log(error);
            });
    }
}

export default {
    state: state,
    getters: getters,
    mutations: mutations,
    actions: actions
};