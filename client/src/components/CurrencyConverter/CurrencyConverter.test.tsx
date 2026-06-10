import { describe, test, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { CurrencyConverter } from "./CurrencyConverter";

describe("CurrencyConverter", () => {
    test("renders selects and input fields with mock data", () => {
        render(<CurrencyConverter />);

        const selects = screen.getAllByRole("combobox");
        expect(selects).toHaveLength(2);

        const textboxes = screen.getAllByRole("textbox");
        expect(textboxes).toHaveLength(2);
    });

    test("selects contain all currency options", () => {
        render(<CurrencyConverter />);

        const selects = screen.getAllByRole("combobox");
        const sourceSelect = selects[0];
        const options = sourceSelect.querySelectorAll("option");

        expect(options).toHaveLength(5);
    });

    test("recalculates result when amount changes", async () => {
        const user = userEvent.setup();

        render(<CurrencyConverter />);

        const textboxes = screen.getAllByRole("textbox");
        const amountInput = textboxes[0];
        const resultInput = textboxes[1] as HTMLInputElement;

        expect(resultInput.value).toBe("2.95");

        await user.clear(amountInput);
        await user.type(amountInput, "10");

        expect(resultInput.value).toBe("29.50");
    });

    test("recalculates result when currency pair changes", async () => {
        const user = userEvent.setup();

        render(<CurrencyConverter />);

        const selects = screen.getAllByRole("combobox");
        const targetSelect = selects[1];
        const textboxes = screen.getAllByRole("textbox");
        const resultInput = textboxes[1] as HTMLInputElement;

        expect(resultInput.value).toBe("2.95");

        await user.selectOptions(targetSelect, "AUD");

        expect(resultInput.value).toBe("1.11");
    });

    test("prevents selecting the same currency in both selects", async () => {
        const user = userEvent.setup();

        render(<CurrencyConverter />);

        const selects = screen.getAllByRole("combobox");
        const sourceSelect = selects[0] as HTMLSelectElement;
        const targetSelect = selects[1] as HTMLSelectElement;

        expect(sourceSelect.value).toBe("CAD");
        expect(targetSelect.value).toBe("PLN");

        await user.selectOptions(sourceSelect, "PLN");

        expect(sourceSelect.value).toBe("PLN");
        expect(targetSelect.value).not.toBe("PLN");
    });

    test("swap exchanges from and to currencies", async () => {
        const user = userEvent.setup();

        render(<CurrencyConverter />);

        const selects = screen.getAllByRole("combobox");
        const sourceSelect = selects[0] as HTMLSelectElement;
        const targetSelect = selects[1] as HTMLSelectElement;

        const initialFrom = sourceSelect.value;
        const initialTo = targetSelect.value;

        const swapButton = screen.getByRole("button", { name: "⇅" });
        await user.click(swapButton);

        expect(sourceSelect.value).toBe(initialTo);
        expect(targetSelect.value).toBe(initialFrom);
    });

    test("resets MoreAbout open state when currency pair changes (key reset)", async () => {
        const user = userEvent.setup();

        render(<CurrencyConverter />);

        const moreAboutButton = screen.getByRole("button", {
            name: /about/i,
        });
        await user.click(moreAboutButton);

        const selects = screen.getAllByRole("combobox");
        const sourceSelect = selects[0] as HTMLSelectElement;
        const fromCode = sourceSelect.value;

        expect(
            screen.getByTestId(`currency-info-${fromCode}`)
        ).toBeInTheDocument();

        const targetSelect = selects[1] as HTMLSelectElement;
        const currentTo = targetSelect.value;
        const newTo = currentTo === "AUD" ? "JPY" : "AUD";

        await user.selectOptions(targetSelect, newTo);

        expect(
            screen.queryByTestId(`currency-info-${fromCode}`)
        ).not.toBeInTheDocument();
    });
});