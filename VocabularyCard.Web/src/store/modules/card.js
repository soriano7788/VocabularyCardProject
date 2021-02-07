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
    },
    updateCard: ({ commit }, card) => {
        return axios.put("card/update", card)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    // 還需要更新目前 state 裡面的 Card 資料

                    Promise.resolve();
                }

            }).catch(error => {
                Promise.reject();
            });
    }
}

export default {
    state: state,
    getters: getters,
    mutations: mutations,
    actions: actions
};