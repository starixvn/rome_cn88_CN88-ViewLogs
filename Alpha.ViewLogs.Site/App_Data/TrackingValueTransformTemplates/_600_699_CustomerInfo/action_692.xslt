<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="r">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'SubAccName'" />
        <xsl:with-param name="firstValue" select="@subacc" />

        <xsl:with-param name="secondKey" select="'NewManagedAccount'" />
        <xsl:with-param name="secondValue" select="@newmanaged" />

        <xsl:with-param name="thirdKey" select="'IP'" />
        <xsl:with-param name="thirdValue" select="xslLogFormat:FormatIP(@ip)" />
      </xsl:call-template>
    </table>
  </xsl:template>
</xsl:stylesheet>