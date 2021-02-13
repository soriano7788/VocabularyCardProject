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
    },
    updateCard: (state, card) => {
        var target = state.cards.find(function (item) {
            return item.CardId == card.CardId;
        });

        if (target == undefined || target == null) {
            state.cards.push(card);
        } else {
            Object.assign(target, card);
            // target = card;
        }
    },
    deleteCard: (state, cardId) => {
        const target = state.cards.find(function (item) {
            return item.Id == cardId;
        });

        if (target != undefined && target != null) {
            const index = state.cards.indexOf(target);
            if (index != -1) {
                state.cards.splice(index, 1);
            }
        }
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
                    commit("updateCard", card);
                }
                Promise.resolve();
            }).catch(error => {
                Promise.reject();
            });
    },
    deleteCard: ({ commit }, cardId) => {
        return axios.delete("card/delete/" + cardId)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    commit("deleteCard", cardId);
                }
                Promise.resolve();
            })
            .catch(error => {
                console.log("ajax deleteCard error", error);
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