private void btnSalvar_Click(object sender, EventArgs e)
        {
            Document documento = new Document(PageSize.A4);
            documento.SetMargins(3, 2, 3, 2);
            documento.AddCreationDate();

            string nome_pdf = txtCliente.Text + " " + lblNomeCli.Text + "-pedido";

            string vArq = "";
            MessageBox.Show("Selecione um caminho para salvar o pedido", "Salvar Pedido",MessageBoxButtons.OK);
            FolderBrowserDialog vSalvar = new FolderBrowserDialog();

            if (vSalvar.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                vArq = vSalvar.SelectedPath + "\\" + nome_pdf.Trim() + ".pdf";
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"C:\digiplascomercial\imagens\LOGO.png");
                logo.ScalePercent(8f);
                logo.Alignment = 5;
                PdfPTable dados = new PdfPTable(1);

                iTextSharp.text.Font fonte = FontFactory.GetFont(BaseFont.TIMES_BOLD);

                Paragraph coluna1 = new Paragraph("Cod. do Cliente(FKN): " + txtFkn.Text);
                Paragraph coluna2 = new Paragraph("Nome do Cliente: " + lblNomeCli.Text);
                Paragraph coluna3 = new Paragraph("Cod. do Cliente " + txtCliente.Text);
                var cell1 = new PdfPCell();
                cell1.AddElement(coluna1);
                dados.AddCell(cell1);
                var cell2 = new PdfPCell();
                cell2.AddElement(coluna2);
                dados.AddCell(cell2);
                var cell3 = new PdfPCell();
                cell3.AddElement(coluna3);
                dados.AddCell(cell3);
                
                PdfPTable ped_cabecalho = new PdfPTable(4);
                Paragraph c1 = new Paragraph("Produtos", fonte);
                Paragraph c2 = new Paragraph("Qtd", fonte);
                Paragraph c3 = new Paragraph("Preço Uni", fonte);
                Paragraph c4 = new Paragraph("Preço Total", fonte);
                var cel1 = new PdfPCell();
                cel1.AddElement(c1);
                ped_cabecalho.AddCell(cel1);
                var cel2 = new PdfPCell();
                cel2.AddElement(c2);
                ped_cabecalho.AddCell(cel2);
                var cel3 = new PdfPCell();
                cel3.AddElement(c3);
                ped_cabecalho.AddCell(cel3);
                var cel4 = new PdfPCell();
                cel4.AddElement(c4);
                ped_cabecalho.AddCell(cel4);
                int qtd_total = gridCarrinho.RowCount;

                PdfPTable ped = new PdfPTable(4);

                for (int i=0; i < qtd_total; i++)
                {
                    addProduto(i, ped);
                }

                string cabecalho = "\n---------------------------------------------------------------Dados do Pedido-------------------------------------------------------------\n\n";
                
                string separador = "\n---------------------------------------------------------------------------------------------------------------------------------------------------\n\n";

                string entrega = "\n---------------------------------------------------------------Local de Entrega-------------------------------------------------------------\n\n";
            
                string assinatura = "Nome do cliente:" + lblNomeCli.Text +"\n\n";
                assinatura += "______________________________________.\n";
                assinatura += "              Recepção/Caixa";
				
                PdfWriter.GetInstance(documento, new FileStream(nome_pdf, FileMode.Create));



                documento.Open();
                documento.Add(logo);
                documento.Add(dados);
                documento.Add(new Paragraph(cabecalho));
                documento.Add(ped_cabecalho);
                documento.Add(ped);
                documento.Add(new Paragraph(entrega));
                documento.Add(new Paragraph("ENDEREÇO AQUI"));
                documento.Add(new Paragraph(separador));
                documento.Add(new Paragraph("\n"));
                documento.Add(new Paragraph("\n"));
                documento.Add(new Paragraph("\n"));
                documento.Add(new Paragraph(assinatura));
                
                documento.Close();

                File.Move(nome_pdf, vArq);
            }
			
            Close();
        }
		
private void addProduto(int p, PdfPTable ped)
        {
            Paragraph r1 = new Paragraph(gridCarrinho.Rows[p].Cells[1].Value.ToString());
            Paragraph r2 = new Paragraph(gridCarrinho.Rows[p].Cells[2].Value.ToString());
            Paragraph r3 = new Paragraph(gridCarrinho.Rows[p].Cells[3].Value.ToString());
            Paragraph r4 = new Paragraph(gridCarrinho.Rows[p].Cells[4].Value.ToString());
            var a1 = new PdfPCell();
            a1.AddElement(r1);
            ped.AddCell(a1);
            var a2 = new PdfPCell();
            a2.AddElement(r2);
            ped.AddCell(a2);
            var a3 = new PdfPCell();
            a3.AddElement(r3);
            ped.AddCell(a3);
            var a4 = new PdfPCell();
            a4.AddElement(r4);
            ped.AddCell(a4);
        }