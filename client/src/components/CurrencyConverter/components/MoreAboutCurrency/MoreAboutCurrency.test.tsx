import { describe, test, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MoreAboutCurrency } from "./MoreAboutCurrency";

const fromCurrency = "PLN";
const toCurrency = "JPY";

describe("MoreAboutCurrency", () => {
    test("renders button", () => {
        render(<MoreAboutCurrency fromCurrency={fromCurrency} toCurrency={toCurrency} />);

        expect(screen.getByRole("button", { name: new RegExp(`${fromCurrency}/${toCurrency}: about`, "i") })).toBeInTheDocument();
    });

    test("description is hidden by default", () => {
        render(<MoreAboutCurrency fromCurrency={fromCurrency} toCurrency={toCurrency} />);

        expect(screen.queryByTestId(`currency-info-${fromCurrency}`)).not.toBeInTheDocument();
        expect(screen.queryByTestId(`currency-info-${toCurrency}`)).not.toBeInTheDocument();
    });

    test("shows description after click", async () => {
        const user = userEvent.setup();

        render(<MoreAboutCurrency fromCurrency={fromCurrency} toCurrency={toCurrency} />);

        const button = screen.getByRole("button", { name: new RegExp(`${fromCurrency}/${toCurrency}: about`, "i") });

        await user.click(button);

        expect(screen.getByTestId(`currency-info-${fromCurrency}`)).toBeInTheDocument();
        expect(screen.getByTestId(`currency-info-${toCurrency}`)).toBeInTheDocument();
    });

    test("hides description after second click", async () => {
        const user = userEvent.setup();

        render(<MoreAboutCurrency fromCurrency={fromCurrency} toCurrency={toCurrency} />);

        const button = screen.getByRole("button", { name: new RegExp(`${fromCurrency}/${toCurrency}: about`, "i") });

        await user.click(button);

        expect(screen.getByTestId(`currency-info-${fromCurrency}`)).toBeInTheDocument();

        await user.click(button);

        expect(screen.queryByTestId(`currency-info-${fromCurrency}`)).not.toBeInTheDocument();
        expect(screen.queryByTestId(`currency-info-${toCurrency}`)).not.toBeInTheDocument();
    });
});