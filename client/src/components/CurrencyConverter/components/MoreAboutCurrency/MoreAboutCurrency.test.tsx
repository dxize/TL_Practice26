import { describe, test, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MoreAboutCurrency } from "./MoreAboutCurrency";
import { currencies } from "../../../../mocks";

const fromCurrencyObj = currencies.find(c => c.code === "PLN")!;
const toCurrencyObj = currencies.find(c => c.code === "JPY")!;
const fromCode = "PLN";
const toCode = "JPY";

describe("MoreAboutCurrency", () => {
    test("renders button", () => {
        render(<MoreAboutCurrency fromCurrency={fromCurrencyObj} toCurrency={toCurrencyObj} />);

        expect(screen.getByRole("button", { name: new RegExp(`${fromCode}/${toCode}: about`, "i") })).toBeInTheDocument();
    });

    test("description is hidden by default", () => {
        render(<MoreAboutCurrency fromCurrency={fromCurrencyObj} toCurrency={toCurrencyObj} />);

        expect(screen.queryByTestId(`currency-info-${fromCode}`)).not.toBeInTheDocument();
        expect(screen.queryByTestId(`currency-info-${toCode}`)).not.toBeInTheDocument();
    });

    test("shows description after click", async () => {
        const user = userEvent.setup();

        render(<MoreAboutCurrency fromCurrency={fromCurrencyObj} toCurrency={toCurrencyObj} />);

        const button = screen.getByRole("button", { name: new RegExp(`${fromCode}/${toCode}: about`, "i") });

        await user.click(button);

        expect(screen.getByTestId(`currency-info-${fromCode}`)).toBeInTheDocument();
        expect(screen.getByTestId(`currency-info-${toCode}`)).toBeInTheDocument();
    });

    test("hides description after second click", async () => {
        const user = userEvent.setup();

        render(<MoreAboutCurrency fromCurrency={fromCurrencyObj} toCurrency={toCurrencyObj} />);

        const button = screen.getByRole("button", { name: new RegExp(`${fromCode}/${toCode}: about`, "i") });

        await user.click(button);

        expect(screen.getByTestId(`currency-info-${fromCode}`)).toBeInTheDocument();

        await user.click(button);

        expect(screen.queryByTestId(`currency-info-${fromCode}`)).not.toBeInTheDocument();
        expect(screen.queryByTestId(`currency-info-${toCode}`)).not.toBeInTheDocument();
    });
});